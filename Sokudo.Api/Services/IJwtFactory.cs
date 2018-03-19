using Sokudo.Domain.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sokudo.Api.Services
{
    public interface IJwtFactory
    {
        Task<JwtToken> GenerateTokenAsync(User user);
    }
}
