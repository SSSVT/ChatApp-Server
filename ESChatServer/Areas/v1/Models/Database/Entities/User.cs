using ESChatServer.Areas.v1.Models.Application.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ESChatServer.Areas.v1.Models.Database.Entities
{
    public class User : RegistrationModel
    {
        public User()
        {

        }
        public User(RegistrationModel registration)
        {
            this.FirstName = registration.FirstName;
            this.MiddleName = registration.MiddleName;
            this.LastName = registration.LastName;
            this.Birthdate = registration.Birthdate;
            this.Gender = registration.Gender;
            this.Email = registration.Email;
            this.Username = registration.Username;
        }

        [Required]
        public long ID { get; set; }

        [JsonIgnore]
        [Required, MaxLength(2048)]
        public string PasswordHash { get; set; }

        [JsonIgnore]
        [Required, MaxLength(2048)]
        public string PasswordSalt { get; set; }

        [Required]
        public DateTime? UTCRegistrationDate { get; set; }

        [Required, MaxLength(1)]
        [RegularExpression("[ADIO]")] //Active, Do not disturb, Invisible, Offline
        public string Status { get; set; }

        #region Virtual
        [JsonIgnore]
        public virtual ICollection<Room> OwnedRooms { get; set; }
        [JsonIgnore]
        public virtual ICollection<Participant> Participants { get; set; }
        [JsonIgnore]
        public virtual ICollection<Message> Messages { get; set; }
        [JsonIgnore]
        public virtual ICollection<Friendship> SentFriendships { get; set; }
        [JsonIgnore]
        public virtual ICollection<Friendship> ReceivedFriendships { get; set; }

        [JsonIgnore]
        public virtual ICollection<PasswordReset> PasswordResets { get; set; }
        #endregion
    }
}
