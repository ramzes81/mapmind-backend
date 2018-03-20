using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MapMind.Api.Response.Auth
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