using System;
using System.ComponentModel.DataAnnotations;

namespace ESChatServer.Areas.v1.Models.Application.Entities
{
    public class RegistrationModel
    {
        [Required, MaxLength(64)]
        [RegularExpression(".+")]
        public string FirstName { get; set; }

        [MaxLength(64)]
        [RegularExpression(".+")]
        public string MiddleName { get; set; }

        [Required, MaxLength(64)]
        [RegularExpression(".+")]
        public string LastName { get; set; }

        [Required]
        public DateTime? Birthdate { get; set; }

        [Required, MaxLength(1)]
        [RegularExpression("[MF]")]
        public string Gender { get; set; }

        [Required, MaxLength(512)]
        [EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(8), MaxLength(64)]
        [RegularExpression(".+")]
        public string Username { get; set; }

        [Required, MinLength(8), MaxLength(128)]
        [RegularExpression(".+")]
        public string Password { get; set; }
    }
}
