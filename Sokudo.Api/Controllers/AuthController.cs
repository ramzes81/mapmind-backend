using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sokudo.Api.Models.Auth;
using Sokudo.Domain.Authentication;

namespace Sokudo.Api.Controllers
{
    public class AuthController : ControllerBase
    {
        private SignInManager<User> _signManager;
        private UserManager<User> _userManager;

        public AuthController(UserManager<User> userManager, SignInManager<User> signManager)
        {
            _userManager = userManager;
            _signManager = signManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register(SignUpViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var user = new User { Email = model.Email };
            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                await _signManager.SignInAsync(user, false);
            }
            else
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
