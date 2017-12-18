using System.ComponentModel.DataAnnotations;

namespace ESChatServer.Areas.v1.Models.Application.Entities
{
    public class Login
    {
        [Required, MaxLength(64)]
        public string Username { get; set; }
        [Required, MaxLength(128)]
        public string Password { get; set; }
    }
}
