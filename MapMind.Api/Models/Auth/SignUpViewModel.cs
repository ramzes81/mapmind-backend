using System.ComponentModel.DataAnnotations;

namespace MapMind.Api.Models.Auth
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "You must add email")]
        [EmailAddress]
        public string Email { get; set; }
    }
}