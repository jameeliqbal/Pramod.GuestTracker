﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pramod.GuestTracker.Web.Models
{
    public class Guest
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool IsVip { get; set; }
    }
}