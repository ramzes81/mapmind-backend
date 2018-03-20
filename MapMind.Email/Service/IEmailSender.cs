using System.Threading.Tasks;

namespace MapMind.Email.Service
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
