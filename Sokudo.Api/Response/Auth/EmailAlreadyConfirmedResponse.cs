using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sokudo.Api.Response.Auth
{
    public class EmailAlreadyConfirmedResponse: BadRequestResponse
    {
        public override string Message => "Email already confirmed!";
    }
}
