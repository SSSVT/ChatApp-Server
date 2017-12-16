using System.ComponentModel.DataAnnotations;

namespace ESChatServer.Areas.v1.Models.Application.Entities
{
    public class Login
    {
        [MaxLength(64), Required]
        public string Username { get; set; }
        [MaxLength(128), Required]
        public string Password { get; set; }
    }
}
