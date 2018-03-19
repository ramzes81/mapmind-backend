using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sokudo.Api.Response.Auth
{
    public class UserAlreadyFinishedRegistrationResponse : BadRequestResponse
    {
        public override string Message => "User already finished registration.";
    }
}
