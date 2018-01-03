using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ESChatServer.Areas.v1.Models.Database.Entities
{
    public class Message
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        public long IDRoom { get; set; }

        [Required]
        public long IDUser { get; set; }

        [Required]
        public DateTime? UTCSend { get; set; }

        public DateTime? UTCServerReceived { get; set; }

        [Required]
        public string Content { get; set; }

        #region Virtual
        [JsonIgnore]
        public virtual Room Room { get; set; }
        [JsonIgnore]
        public virtual User Owner { get; set; }
        #endregion
    }
}
