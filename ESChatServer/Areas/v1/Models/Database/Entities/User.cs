using ESChatServer.Areas.v1.Models.Application.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ESChatServer.Areas.v1.Models.Database.Entities
{
    public class User : Registration
    {
        public User()
        {

        }
        public User(Registration registration)
        {
            this.FirstName = registration.FirstName;
            this.MiddleName = registration.MiddleName;
            this.LastName = registration.LastName;
            this.Birthday = registration.Birthday;
            this.Gender = registration.Gender;
            this.Email = registration.Email;
            this.Username = registration.Username;
        }

        [JsonIgnore]
        [Required]
        public long ID { get; set; }

        [JsonIgnore]
        [Required, MaxLength(2048)]
        public string PasswordHash { get; set; }

        [JsonIgnore]
        [Required, MaxLength(2048)]
        public string PasswordSalt { get; set; }

        [Required]
        public DateTime UTCRegistrationDate { get; set; }

        [Required, MaxLength(1)]
        [RegularExpression("[ADIO]")] //Active, Do not disturn, Invisible, Offline
        public string Status { get; set; }

        public ICollection<Login> Logins { get; set; }
        public ICollection<Room> OwnedRooms { get; set; }
        public ICollection<Participant> Participants { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
