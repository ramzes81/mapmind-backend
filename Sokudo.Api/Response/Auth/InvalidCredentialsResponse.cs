using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sokudo.Api.Response.Auth
{
    public class InvalidCredentialsResponse : BadRequestResponse
    {
        public override string Message => "Invalid email/password.";
    }
}
