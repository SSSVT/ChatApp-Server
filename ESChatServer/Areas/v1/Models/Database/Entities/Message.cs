using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

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

        public string Sender
        {
            get
            {
                StringBuilder sb = new StringBuilder(192);
                sb.AppendFormat("{0} {1}", this.Owner.FirstName, this.Owner.LastName);
                return sb.ToString();
            }
        }

        #region Virtual
        [JsonIgnore]
        public virtual Room Room { get; set; }
        [JsonIgnore]
        public virtual User Owner { get; set; }
        #endregion
    }
}
