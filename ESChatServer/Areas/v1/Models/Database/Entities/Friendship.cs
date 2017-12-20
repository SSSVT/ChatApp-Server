using System;
using System.ComponentModel.DataAnnotations;

namespace ESChatServer.Areas.v1.Models.Database.Entities
{
    public class Friendship
    {
        [Required]
        public Guid ID { get; set; }
        public long IDSender { get; set; }
        public long IDRecipient { get; set; }

        public DateTime UTCSend { get; set; }
        public DateTime UTCAccepted { get; set; }

        public virtual User Sender { get; set; }
        public virtual User Recipient { get; set; }
    }
}
