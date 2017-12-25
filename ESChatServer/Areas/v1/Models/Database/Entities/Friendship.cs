using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ESChatServer.Areas.v1.Models.Database.Entities
{
    public class Friendship
    {
        [Required]
        public Guid ID { get; set; }

        [JsonIgnore]
        [Required]
        public long IDSender { get; set; }

        [JsonIgnore]
        [Required]
        public long IDRecipient { get; set; }

        [Required]
        public DateTime? UTCSend { get; set; }

        [Required]
        public DateTime? UTCAccepted { get; set; }

        public virtual User Sender { get; set; }
        public virtual User Recipient { get; set; }
    }
}
