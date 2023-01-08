using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Pramod.GuestTracker.Web.Models
{
    public class WAMessageStatus
    {
        [Key]
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Type { get; set; }
        public string Timestamp { get; set; }
        public string ConversationId { get; set; }
        public string ConversationOrigin { get; set; }
        public string Remarks { get; set; }


    }
}