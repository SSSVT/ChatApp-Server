using System;
using System.ComponentModel.DataAnnotations;

namespace ESChatServer.Areas.v1.Models.Application.Entities
{
    public class Registration
    {
        [Required, MaxLength(64)]
        public string FirstName { get; set; }
        [MaxLength(64)]
        public string MiddleName { get; set; }
        [Required, MaxLength(64)]
        public string LastName { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required, MaxLength(1)]
        public string Gender { get; set; }

        [Required, MaxLength(64)]
        public string Username { get; set; }
        [Required, MaxLength(128)]
        public string Password { get; set; }
    }
}
