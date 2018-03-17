using Sokudo.Api.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sokudo.Api.Response.Auth
{
    public class SuccessfulLoginResponse: OkResponse
    {
        public SuccessfulLoginResponse(UserViewModel userViewModel, IEnumerable<string> roles)
        {
            User = userViewModel;
            Roles = roles;
        }

        public UserViewModel User { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
