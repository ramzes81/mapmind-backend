using Sokudo.Service.Account.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sokudo.Service.Account
{
    public interface IEmailConfirmationService
    {
        Task<EmailConfirmationResult> ConfirmEmailAsync(string userId, string confirmationCode);
    }
}
