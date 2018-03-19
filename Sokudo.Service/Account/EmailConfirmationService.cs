using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Sokudo.Domain.Authentication;
using Sokudo.Service.Account.Result;

namespace Sokudo.Service.Account
{
    public class EmailConfirmationService : IEmailConfirmationService
    {
        private readonly UserManager<User> _userManager;

        public EmailConfirmationService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<EmailConfirmationResult> ConfirmEmailAsync(string userId, string confirmationCode)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new EmailConfirmationResult(EmailConfirmationErrorResultCodes.UserNotFound);
            }
            if (user.EmailConfirmed)
            {
                return new EmailConfirmationResult(EmailConfirmationErrorResultCodes.EmailAlreadyConfirmed);
            }
            var result = await _userManager.ConfirmEmailAsync(user, confirmationCode);
            if (!result.Succeeded)
            {
                return new EmailConfirmationResult(EmailConfirmationErrorResultCodes.InvalidToken);
            }
            return new EmailConfirmationResult();
        }
    }
}
