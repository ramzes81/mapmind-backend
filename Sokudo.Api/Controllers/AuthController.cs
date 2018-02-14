using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sokudo.Api.Models.Auth;
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

        public AuthController(UserManager<User> userManager, SignInManager<User> signManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signManager = signManager;
            _emailService = emailService;
        }

        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register(SignUpViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var user = new User { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user);

            //if (result.Succeeded)
            {
                
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Link("https://sokudo.ml",
                    new { userId = user.Id, code = code });

                await _emailService.SendConfirmationEmailAsync(model.Email, callbackUrl);
            }
            //else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return Ok();
        }
    }
}
