using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ESChatServer.Areas.v1.Models.Database.Entities
{
    public class Friendship
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        public long IDSender { get; set; }

        [Required]
        public long IDRecipient { get; set; }

        [Required]
        public DateTime? UTCServerReceived { get; set; }

        [Required]
        public DateTime? UTCAccepted { get; set; }

        #region Virtual
        [JsonIgnore]
        public virtual User Sender { get; set; }
        [JsonIgnore]
        public virtual User Recipient { get; set; }
        #endregion
    }
}
