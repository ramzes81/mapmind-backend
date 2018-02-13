using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sokudo.Api.Models.Auth
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "You must add email")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
