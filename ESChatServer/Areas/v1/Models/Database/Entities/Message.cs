using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ESChatServer.Areas.v1.Models.Database.Entities
{
    public class Message
    {
        [Required]
        public Guid ID { get; set; }

        [JsonIgnore]
        [Required]
        public long IDRoom { get; set; }

        [JsonIgnore]
        [Required]
        public long IDUser { get; set; }

        [Required]
        public DateTime? UTCSend { get; set; }

        [Required]
        public string Content { get; set; }

        public virtual Room Room { get; set; }
        public virtual User Owner { get; set; }
    }
}
