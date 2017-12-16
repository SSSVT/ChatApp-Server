using Newtonsoft.Json;
using System;

namespace ESChatServer.Areas.v1.Models.Database.Entities
{
    public class Login
    {
        [JsonIgnore]
        public Guid ID { get; set; }
        [JsonIgnore]
        public long IDUser { get; set; }

        public DateTime UTCLoginTime { get; set; }
        public DateTime UTCLogoutTime { get; set; }

        public string UserAgent { get; set; }
        public string IPAddress { get; set; }

        public virtual User User { get; set; }
    }
}
