﻿using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ESChatServer.Areas.v1.Models.Database.Entities
{
    public class Participant
    {
        [JsonIgnore]
        [Required]
        public Guid ID { get; set; }

        [Required]
        public long IDRoom { get; set; }

        [Required]
        public long IDUser { get; set; }

        [Required]
        public DateTime UTCJoinDate { get; set; }

        public DateTime UTCLeftDate { get; set; }

        public virtual Room Room { get; set; }
        public virtual User User { get; set; }
    }
}
