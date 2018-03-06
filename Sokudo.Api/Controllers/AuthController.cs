using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using AspNetCoreIdentityBoilerplate.Controller;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Sokudo.Api.Models.Auth;
using Sokudo.Api.Response;
using Sokudo.Api.Response.Auth;
using Sokudo.Api.Settings;
using Sokudo.Domain.Authentication;
using Sokudo.Email.Service;

namespace Sokudo.Api.Controllers
{
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class AuthController : BaseIdentityController<User>
    {
        private readonly SignInManager<User> _signManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<User> _roleManager;
        private readonly IEmailService _emailService;
        private readonly HostSettings _hostSettings;
        private readonly EmailConfirmationSettings _emailConfirmationSettings;

        public AuthController(UserManager<User> userManager, SignInManager<User> signManager, 
            IEmailService emailService, IOptions<HostSettings> hostSettingsProvider,
            IOptions<EmailConfirmationSettings> emailConfirmationSettingsProvider) : base(userManager)
        {
            _userManager = userManager;
            _signManager = signManager;
            _emailService = emailService;
            _hostSettings = hostSettingsProvider.Value;
            _emailConfirmationSettings = emailConfirmationSettingsProvider.Value;
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
                return Ok();
            }
            
            ModelState.AddModelError("", "Invalid login/password");
            
            return BadRequest();
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
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return BadRequest();
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
            var user = await GetCurrentUserAsync();
            return Ok();
        }

        [HttpPost(nameof(CompleteRegistrationDriver))]
        public async Task<IActionResult> CompleteRegistrationDriver([FromBody] DriverCompleteRegistrationModel model)
        {
            return Ok();
        }

        [HttpPost(nameof(ConfirmEmail))]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailModel model)
        {
            // FOR TESTING PURPOSES
            var user = await _userManager.FindByIdAsync(model.UserId);
            await _signManager.SignInAsync(user, true);
            return Ok();
            //var user = await _userManager.FindByIdAsync(model.UserId);
            //if (user == null)
            //{
            //    return NotFound(new NotFoundResponse<User>());
            //}
            ////if(user.EmailConfirmed)
            ////{
            ////    return BadRequest(new EmailAlreadyConfirmedResponse());
            ////}
            //var result = await _userManager.ConfirmEmailAsync(user, model.Code);
            //if (!result.Succeeded)
            //{
            //    return BadRequest(new CantConfirmEmailResponse());
            //}
            //await _signManager.SignInAsync(user, true);
            //return Ok();
        }
    }
}
