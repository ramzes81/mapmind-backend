using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using AspNetCoreIdentityBoilerplate.Controller;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Sokudo.Api.Models.Auth;
using Sokudo.Api.Response;
using Sokudo.Api.Response.Auth;
using Sokudo.Api.Settings;
using Sokudo.Api.ViewModels.User;
using Sokudo.Domain.Authentication;
using Sokudo.Email.Service;
using Sokudo.Service.Account;
using Sokudo.Service.Account.Result;

namespace Sokudo.Api.Controllers
{
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class AuthController : BaseIdentityController<User>
    {
        private readonly SignInManager<User> _signManager;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly HostSettings _hostSettings;
        private readonly EmailConfirmationSettings _emailConfirmationSettings;
        private readonly IMapper _mapper;
        private readonly IEmailConfirmationService _emailConfirmationService;

        public AuthController(UserManager<User> userManager, SignInManager<User> signManager,
            IEmailService emailService, IOptions<HostSettings> hostSettingsProvider,
            IOptions<EmailConfirmationSettings> emailConfirmationSettingsProvider,
            IMapper mapper, IEmailConfirmationService emailConfirmationService) : base(userManager)
        {
            _userManager = userManager;
            _signManager = signManager;
            _emailService = emailService;
            _hostSettings = hostSettingsProvider.Value;
            _emailConfirmationSettings = emailConfirmationSettingsProvider.Value;
            _mapper = mapper;
            _emailConfirmationService = emailConfirmationService;
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(!user.EmailConfirmed)
            {
                //
            }
            if (!user.IsRegistrationFinished)
            {
                //
            }
            
            var signInResult = 
                await _signManager.PasswordSignInAsync(user, model.Password, true, false);
            
            if(signInResult.Succeeded)
            {
                var userViewModel = _mapper.Map<UserViewModel>(user);
                var roles = await _userManager.GetRolesAsync(user);
                return Ok(new SuccessfulLoginResponse(userViewModel, roles));
            }

            return BadRequest(new InvalidCredentialsResponse());
        }

        [HttpPost(nameof(LogOut))]
        public async Task<IActionResult> LogOut()
        {
            await _signManager.SignOutAsync();
            return Ok();
        }

        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register([FromBody] SignUpViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                user = new User { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user);

                if(!result.Succeeded)
                {
                    return BadRequest(new IdentityBadRequestResponse(ModelState, result));
                }
            }

            if(user.IsRegistrationFinished)
            {
                return BadRequest(new UserAlreadyFinishedRegistrationResponse());
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters.Add("userId", user.Id);
            parameters.Add("code", code);
            var urlBuilder = new UriBuilder(_hostSettings.BaseUrl)
            {
                Path = _emailConfirmationSettings.Path,
                Query = parameters.ToString()
            };

            var callbackUrl = urlBuilder.Uri.ToString();

            await _emailService.SendConfirmationEmailAsync(model.Email, callbackUrl);

            return Ok();
        }

        [HttpPost(nameof(CompleteRegistrationPassenger))]
        public async Task<IActionResult> CompleteRegistrationPassenger([FromBody] CompleteRegistrationModel model)
        {
            var confirmEmailResult = await ConfirmEmail(model.UserId, model.ConfirmationCode);
            if(confirmEmailResult != null)
            {
                return confirmEmailResult;
            }
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user.IsRegistrationFinished)
            {
                return BadRequest(new RegistrationAlreadyFinishedResponse());
            }

            var result = await _userManager.AddPasswordAsync(user, model.Password);

            if(!result.Succeeded)
            {
                return BadRequest(new IdentityBadRequestResponse(ModelState, result));
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.IsRegistrationFinished = true;
            
            await _userManager.UpdateAsync(user);

            await _userManager.AddToRolesAsync(user, new[] { UserRoles.User, UserRoles.Passenger });

            return Ok();
        }

        [Authorize]
        [HttpPost(nameof(CompleteRegistrationDriver))]
        public async Task<IActionResult> CompleteRegistrationDriver([FromBody] DriverCompleteRegistrationModel model)
        {
            var user = await GetCurrentUserAsync();

            if (user.IsRegistrationFinished)
            {
                return BadRequest(new RegistrationAlreadyFinishedResponse());
            }

            var result = await _userManager.AddPasswordAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new IdentityBadRequestResponse(ModelState, result));
            }

            await _userManager.AddToRolesAsync(user, new[] { UserRoles.User, UserRoles.Passenger });
            return Ok();
        }
        
        private async Task<IActionResult> ConfirmEmail(string userId, string confirmationCode)
        {
            var emailConfirmationResult = await
                _emailConfirmationService.ConfirmEmailAsync(userId, confirmationCode);
            if (!emailConfirmationResult.Success)
            {
                switch (emailConfirmationResult.ResultCode)
                {
                    case EmailConfirmationErrorResultCodes.EmailAlreadyConfirmed:
                        return BadRequest(new EmailAlreadyConfirmedResponse());
                    case EmailConfirmationErrorResultCodes.UserNotFound:
                        return NotFound(new NotFoundResponse<User>());
                    case EmailConfirmationErrorResultCodes.InvalidToken:
                    default:
                        return BadRequest(new CantConfirmEmailResponse());
                }
            }
            return null;
        }
    }
}
