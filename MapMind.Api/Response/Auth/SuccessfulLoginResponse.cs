using System.Collections.Generic;
using MapMind.Api.ViewModels.User;

namespace MapMind.Api.Response.Auth
{
    public class SuccessfulLoginResponse : OkResponse
    {
        public SuccessfulLoginResponse(UserViewModel userViewModel, IEnumerable<string> roles)
        {
            User = userViewModel;
            Roles = roles;
        }

        public UserViewModel User { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public string Token { get; set; }
        public long ExpiresIn { get; set; }
    }
}