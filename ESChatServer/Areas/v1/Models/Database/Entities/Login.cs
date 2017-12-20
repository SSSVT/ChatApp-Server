using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ESChatServer.Areas.v1.Models.Database.Entities
{
    public class Login
    {
        [JsonIgnore]
        [Required]
        public Guid ID { get; set; }

        [JsonIgnore]
        [Required]
        public long IDUser { get; set; }

        [Required]
        public DateTime UTCLoginTime { get; set; }

        public DateTime UTCLogoutTime { get; set; }

        [Required, MaxLength(256)]
        public string UserAgent { get; set; }

        [Required, MaxLength(15)]
        public string IPAddress { get; set; }

        public virtual User User { get; set; }
    }
}
