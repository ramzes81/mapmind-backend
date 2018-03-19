using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sokudo.Api.Response.Auth
{
    public class RegistrationAlreadyFinishedResponse : BadRequestResponse
    {
        public override string Message => "Registration already finished!";
    }
}
