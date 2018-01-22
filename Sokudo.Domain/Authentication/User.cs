using Microsoft.AspNet.Identity.EntityFramework;
using Sokudo.Domain.Profiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sokudo.Domain.Authentication
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual DriverProfile DriverProfile { get; set; }
    }
}
