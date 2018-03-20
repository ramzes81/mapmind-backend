using System.Threading.Tasks;
using MapMind.Domain.Authentication;

namespace MapMind.Api.Services
{
    public interface IJwtFactory
    {
        Task<JwtToken> GenerateTokenAsync(User user);
    }
}