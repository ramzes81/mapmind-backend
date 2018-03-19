using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sokudo.Api.Response.Auth
{
    public class IdentityBadRequestResponse : BadRequestResponse
    {
        private IdentityResult _identityResult;

        public IdentityBadRequestResponse(ModelStateDictionary modelState, IdentityResult identityResult)
        {
            _identityResult = identityResult;

            foreach (var error in identityResult.Errors)
            {
                modelState.AddModelError("", error.Description);
            }
        }

        public override string Message => _identityResult.Errors.First().Description;
    }
}
