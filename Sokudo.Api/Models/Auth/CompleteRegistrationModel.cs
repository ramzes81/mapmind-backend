using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sokudo.Api.Models.Auth
{
    public class CompleteRegistrationModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}
