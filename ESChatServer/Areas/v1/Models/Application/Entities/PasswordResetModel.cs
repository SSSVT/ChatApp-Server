using System;
using System.ComponentModel.DataAnnotations;

namespace ESChatServer.Areas.v1.Models.Application.Entities
{
    public class PasswordResetModel
    {
        [Required]
        public Guid ID { get; set; }

        [Required, MinLength(8), MaxLength(128)]
        [RegularExpression(".+")]
        public string Password { get; set; }
    }
}
