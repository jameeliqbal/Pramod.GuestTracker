﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Pramod.GuestTracker.Web.Data
{
    public class GuestTrackerContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public GuestTrackerContext() : base("name=GuestTrackerContext")
        {
        }

        public System.Data.Entity.DbSet<Pramod.GuestTracker.Web.Models.Guest> Guests { get; set; }

        public System.Data.Entity.DbSet<Pramod.GuestTracker.Web.Models.Attendence> Attendences { get; set; }
    }
}