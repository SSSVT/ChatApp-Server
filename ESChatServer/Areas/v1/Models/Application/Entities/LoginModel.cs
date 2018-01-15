using System.ComponentModel.DataAnnotations;

namespace ESChatServer.Areas.v1.Models.Application.Entities
{
    public class LoginModel
    {
        [Required, MinLength(4), MaxLength(64)]
        [RegularExpression(".+")]
        public string Username { get; set; }

        [Required, MinLength(8), MaxLength(128)]
        [RegularExpression(".+")]
        public string Password { get; set; }
    }
}
