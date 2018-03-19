using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sokudo.Api.Response.Auth
{
    public class CantConfirmEmailResponse : ForbiddenResponse
    {
        public override string Message => "Can't confirm email using provided code";
    }
}
