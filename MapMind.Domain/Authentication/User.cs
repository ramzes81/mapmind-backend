using System.Collections.Generic;
using MapMind.Domain.Map;
using Microsoft.AspNetCore.Identity;

namespace MapMind.Domain.Authentication
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsRegistrationFinished { get; set; }

        public virtual ICollection<MindMap> Maps { get; set; }
    }
}