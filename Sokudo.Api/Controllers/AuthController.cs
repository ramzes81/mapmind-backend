using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Sokudo.Api.Models.Auth;
using Sokudo.Api.Settings;
using Sokudo.Domain.Authentication;
using Sokudo.Email.Service;

namespace Sokudo.Api.Controllers
{
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<User> _signManager;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly HostSettings _hostSettings;
        private readonly EmailConfirmationSettings _emailConfirmationSettings;

        public AuthController(UserManager<User> userManager, SignInManager<User> signManager, 
            IEmailService emailService, IOptions<HostSettings> hostSettingsProvider,
            IOptions<EmailConfirmationSettings> emailConfirmationSettingsProvider)
        {
            _userManager = userManager;
            _signManager = signManager;
            _emailService = emailService;
            _hostSettings = hostSettingsProvider.Value;
            _emailConfirmationSettings = emailConfirmationSettingsProvider.Value;
        }

        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register(SignUpViewModel model)
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

            if(user.EmailConfirmed)
            {
                return BadRequest();
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
    }
}
