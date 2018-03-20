using System.Threading.Tasks;
using MapMind.Service.Account.Result;

namespace MapMind.Service.Account
{
    public interface IEmailConfirmationService
    {
        Task<EmailConfirmationResult> ConfirmEmailAsync(string userId, string confirmationCode);
    }
}