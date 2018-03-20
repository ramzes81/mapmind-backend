using System.Threading.Tasks;

namespace MapMind.Email.Service
{
    public interface IEmailService
    {
        Task SendConfirmationEmailAsync(string email, string confirmationUrl);
    }
}
