using System.ComponentModel.DataAnnotations;

namespace MapMind.Api.Models.Auth
{
    public class ConfirmEmailModel
    {
        [Required] public string UserId { get; set; }

        [Required] public string Code { get; set; }
    }
}