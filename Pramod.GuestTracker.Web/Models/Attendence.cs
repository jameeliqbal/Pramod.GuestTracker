using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pramod.GuestTracker.Web.Models
{
    public class Attendence
    {
        [Key]
        public int Id { get; set; }
        public DateTime ArrivedAt { get; set; }
        public int NumberOfPeople { get; set; }

        public int GuestId { get; set; }
        public Guest Guest { get; set; }
    }
}