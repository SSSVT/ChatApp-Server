using System;
using System.ComponentModel.DataAnnotations;

namespace ESChatServer.Areas.v1.Models.Application.Entities
{
    public class Registration
    {
        [MaxLength(64), Required]
        public string FirstName { get; set; }
        [MaxLength(64)]
        public string MiddleName { get; set; }
        [MaxLength(64), Required]
        public string LastName { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [MaxLength(1), Required]
        public string Gender { get; set; }

        [MaxLength(64), Required]
        public string Username { get; set; }
        [MaxLength(128), Required]
        public string Password { get; set; }
    }
}
