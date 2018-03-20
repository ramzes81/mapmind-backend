using System.Threading.Tasks;

namespace MapMind.Email.Service
{
    public class EmailService : IEmailService
    {
        private readonly IEmailSender _emailSender;

        public EmailService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task SendConfirmationEmailAsync(string email, string confirmationUrl)
        {
            await _emailSender.SendEmailAsync(email, "Confirm your account",
                $"Confirm your email by following this link: <a href='{confirmationUrl}'>link</a>");
        }
    }
}
