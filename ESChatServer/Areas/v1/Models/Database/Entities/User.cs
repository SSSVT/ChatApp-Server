using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESChatServer.Areas.v1.Models.Database.Entities
{
    public class User
    {
        [JsonIgnore]
        [Required]
        public long ID { get; set; }

        [Required, MaxLength(64)]
        [RegularExpression(".")]
        public string FirstName { get; set; }

        [MaxLength(64)]
        [RegularExpression(".")]
        public string MiddleName { get; set; }

        [Required, MaxLength(64)]
        [RegularExpression(".")]
        public string LastName { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required, MaxLength(1)]
        [RegularExpression("[MF]")]
        public string Gender { get; set; }

        [Required, MaxLength(64)]
        public string Username { get; set; }

        [JsonIgnore]
        [Required, MaxLength(2048)]
        public string PasswordHash { get; set; }

        [JsonIgnore]
        [Required, MaxLength(2048)]
        public string PasswordSalt { get; set; }

        [Required]
        public DateTime UTCRegistrationDate { get; set; }

        [Required, MaxLength(1)]
        public string Status { get; set; }

        public ICollection<Login> Logins { get; set; }
        public ICollection<Room> OwnedRooms { get; set; }
        public ICollection<Participant> Participants { get; set; }
    }
}
