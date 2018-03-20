using AutoMapper;

namespace MapMind.Api.ViewModels.User
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public class UserViewModelProfile : Profile
        {
            public UserViewModelProfile()
            {
                CreateMap<Domain.Authentication.User, UserViewModel>();
            }
        }
    }
}