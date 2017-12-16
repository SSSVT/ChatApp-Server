using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ESChatServer.Areas.v1.Models.Database.Entities
{
    public class User
    {
        [JsonIgnore]
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }

        public string Username { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        [JsonIgnore]
        public string PasswordSalt { get; set; }

        public DateTime UTCRegistrationDate { get; set; }

        public ICollection<Login> Logins { get; set; }
    }
}
