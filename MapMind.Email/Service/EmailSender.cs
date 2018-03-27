using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MapMind.Email.Options;
using Microsoft.Extensions.Options;
using MimeKit;

namespace MapMind.Email.Service
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<EmailAuthOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public EmailAuthOptions Options { get; } 

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Mapmind", "no-reply@mapmind.io"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(Options.SmtpServer, Options.SmtpServerPort, true);
                await client.AuthenticateAsync(Options.SenderEmail, Options.SenderEmailPassword);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
