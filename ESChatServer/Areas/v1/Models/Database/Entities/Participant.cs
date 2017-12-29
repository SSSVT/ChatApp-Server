using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ESChatServer.Areas.v1.Models.Database.Entities
{
    public class Participant
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        public long IDRoom { get; set; }

        [Required]
        public long IDUser { get; set; }

        public virtual Room Room { get; set; }
        public virtual User User { get; set; }
    }
}
