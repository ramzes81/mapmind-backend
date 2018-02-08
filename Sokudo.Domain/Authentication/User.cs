using Microsoft.AspNetCore.Identity;

namespace Sokudo.Domain.Authentication
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //public virtual DriverProfile DriverProfile { get; set; }
    }
}
