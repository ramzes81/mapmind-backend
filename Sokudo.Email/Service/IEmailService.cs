using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sokudo.Email.Service
{
    public interface IEmailService
    {
        Task SendConfirmationEmailAsync(string email, string confirmationUrl);
    }
}
