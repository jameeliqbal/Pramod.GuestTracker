using Pramod.GuestTracker.Web.Data;
using Pramod.GuestTracker.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Pramod.GuestTracker.Web.Controllers
{
    public class HomeController : Controller
    {
        private GuestTrackerContext db = new GuestTrackerContext();
        public ActionResult Index(int? id)
        {
            RegisterAttendanceViewModel registerGuest = new RegisterAttendanceViewModel();
            var guest = db.Guests.Find(id);
            if (guest != null)
            {
                registerGuest.GuestId = guest.Id;
                registerGuest.GuestCode = guest.Code;
                registerGuest.GuestName = guest.Name;
                registerGuest.IsVip = guest.IsVip;
                registerGuest.ArrivalTime = DateTime.Now;
            }
            return View(registerGuest);
        }

        [HttpPost]
        public async Task<ActionResult> Index(RegisterAttendanceViewModel newRegistration)
        {
            if (!ModelState.IsValid) return View(newRegistration);

            var attendance = new Models.Attendence();
            attendance.ArrivedAt = newRegistration.ArrivalTime;
            attendance.GuestId = newRegistration.GuestId;
            attendance.NumberOfPeople = newRegistration.NumberOfPeople;

            db.Attendences.Add(attendance);
          var count= await  db.SaveChangesAsync();
            //return View("RegConfirm", newRegistration);
            return RedirectToAction("RegConfirm", "Attendences", newRegistration);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}