using Microsoft.AspNetCore.Identity;
using Sokudo.Domain.Profiles;

namespace Sokudo.Domain.Authentication
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public bool IsRegistrationFinished { get; set; }

        public virtual DriverProfile DriverProfile { get; set; }
    }

    public enum Gender
    {
        NotKnown = 0,
        Male = 1,
        Female = 2,
        NonBinary = 9,
    }
}