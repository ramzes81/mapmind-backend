using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sokudo.Api.Services
{
    public class JwtToken
    {
        public string Token { get; set; }
        public long ExpiresIn { get; set; }
    }
}
