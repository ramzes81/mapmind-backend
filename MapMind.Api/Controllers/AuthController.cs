using AspNetCoreIdentityBoilerplate.Controller;
using AutoMapper;
using MapMind.Api.Models.Auth;
using MapMind.Api.Response;
using MapMind.Api.Response.Auth;
using MapMind.Api.Services;
using MapMind.Api.Settings;
using MapMind.Api.ViewModels.User;
using MapMind.Domain.Authentication;
using MapMind.Email.Service;
using MapMind.Service.Account;
using MapMind.Service.Account.Result;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using System.Web;

namespace MapMind.Api.Controllers
{
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class AuthController : BaseIdentityController<User>
    {
        private readonly IEmailConfirmationService _emailConfirmationService;
        private readonly EmailConfirmationSettings _emailConfirmationSettings;
        private readonly IEmailService _emailService;
        private readonly HostSettings _hostSettings;
        private readonly IJwtFactory _jwtFactory;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signManager;
        private readonly UserManager<User> _userManager;

        public AuthController(UserManager<User> userManager, SignInManager<User> signManager,
            IEmailService emailService, IOptions<HostSettings> hostSettingsProvider,
            IOptions<EmailConfirmationSettings> emailConfirmationSettingsProvider,
            IMapper mapper, IEmailConfirmationService emailConfirmationService,
            IJwtFactory jwtFactory, IOptions<JwtSettings> jwtOptions) : base(userManager)
        {
            _userManager = userManager;
            _signManager = signManager;
            _emailService = emailService;
            _hostSettings = hostSettingsProvider.Value;
            _emailConfirmationSettings = emailConfirmationSettingsProvider.Value;
            _mapper = mapper;
            _emailConfirmationService = emailConfirmationService;
            _jwtFactory = jwtFactory;
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest(new InvalidCredentialsResponse());
            }

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return BadRequest(new InvalidCredentialsResponse());
            }

            var jwt = await _jwtFactory.GenerateTokenAsync(user);
            var userViewModel = _mapper.Map<UserViewModel>(user);
            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new SuccessfulLoginResponse(userViewModel, roles)
            {
                Token = jwt.Token,
                ExpiresIn = jwt.ExpiresIn,
            });
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
            if (user == null)
            {
                user = new User {UserName = model.Email, Email = model.Email};
                var result = await _userManager.CreateAsync(user);

                if (!result.Succeeded)
                {
                    return BadRequest(new IdentityBadRequestResponse(ModelState, result));
                }
            }

            if (user.IsRegistrationFinished)
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

        [HttpPost(nameof(CompleteRegistration))]
        public async Task<IActionResult> CompleteRegistration([FromBody] CompleteRegistrationModel model)
        {
            var confirmEmailResult = await ConfirmEmail(model.UserId, model.ConfirmationCode);
            if (confirmEmailResult != null)
            {
                return confirmEmailResult;
            }

            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user.IsRegistrationFinished)
            {
                return BadRequest(new RegistrationAlreadyFinishedResponse());
            }

            var result = await _userManager.AddPasswordAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new IdentityBadRequestResponse(ModelState, result));
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.IsRegistrationFinished = true;

            await _userManager.UpdateAsync(user);

            await _userManager.AddToRolesAsync(user, new[] {UserRoles.User});

            return Ok();
        }

        private async Task<IActionResult> ConfirmEmail(string userId, string confirmationCode)
        {
            var emailConfirmationResult = await
                _emailConfirmationService.ConfirmEmailAsync(userId, confirmationCode);
            if (emailConfirmationResult.Success)
            {
                return null;
            }
            switch (emailConfirmationResult.ResultCode)
            {
                case EmailConfirmationErrorResultCodes.EmailAlreadyConfirmed:
                    return BadRequest(new EmailAlreadyConfirmedResponse());
                case EmailConfirmationErrorResultCodes.UserNotFound:
                    return NotFound(new NotFoundResponse<User>());
                case EmailConfirmationErrorResultCodes.InvalidToken:
                    return BadRequest(new CantConfirmEmailResponse());
                default:
                    return BadRequest(new CantConfirmEmailResponse());
            }
        }
    }
}