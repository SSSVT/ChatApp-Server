﻿using System.ComponentModel.DataAnnotations;

namespace ESChatServer.Areas.v1.Models.Application.Entities
{
    public class LoginModel
    {
        [Required, MinLength(8), MaxLength(64)]
        public string Username { get; set; }
        [Required, MinLength(8), MaxLength(128)]
        public string Password { get; set; }
    }
}