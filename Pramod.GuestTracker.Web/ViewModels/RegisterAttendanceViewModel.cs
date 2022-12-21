using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pramod.GuestTracker.Web.ViewModels
{
    public class RegisterAttendanceViewModel
    {
        public int GuestId { get; set; }
        public string GuestCode { get; set; }
        public string GuestName { get; set; }
        public DateTime ArrivalTime { get; set; }
        [Required]
        [Display(Name ="Number of People")]
        public int NumberOfPeople { get; set; }
        public bool IsVip { get; set; }


    }
}