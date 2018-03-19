using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sokudo.Api.ViewModels.User
{
    public class UserViewModel
    {
        public class UserViewModelProfile : Profile
        {
            public UserViewModelProfile()
            {
                CreateMap<Domain.Authentication.User, UserViewModel>();
            }
        }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
