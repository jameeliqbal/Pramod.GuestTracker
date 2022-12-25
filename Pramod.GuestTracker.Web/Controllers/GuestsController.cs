using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pramod.GuestTracker.Web.Data;
using Pramod.GuestTracker.Web.Models;
using System.IO;
using CSVLibraryAK;

namespace Pramod.GuestTracker.Web.Controllers
{
    public class GuestsController : Controller
    {
        private GuestTrackerContext db = new GuestTrackerContext();

        // GET: Guests
        public ActionResult Index()
        {
            return View(db.Guests.ToList());
        }

        // GET: Guests
        [HttpGet]
        public ActionResult List()
        {
            return Json(db.Guests.ToList(), JsonRequestBehavior.AllowGet );
        }

        // GET: Guests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guest guest = db.Guests.Find(id);
            if (guest == null)
            {
                return HttpNotFound();
            }
            return View(guest);
        }

        // GET: Guests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Guests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Name,IsVip")] Guest guest)
        {
            if (ModelState.IsValid)
            {
                db.Guests.Add(guest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(guest);
        }

        // GET: Guests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guest guest = db.Guests.Find(id);
            if (guest == null)
            {
                return HttpNotFound();
            }
            return View(guest);
        }

        // POST: Guests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Name,IsVip")] Guest guest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(guest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(guest);
        }

        // GET: Guests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guest guest = db.Guests.Find(id);
            if (guest == null)
            {
                return HttpNotFound();
            }
            return View(guest);
        }

        // POST: Guests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Guest guest = db.Guests.Find(id);
            db.Guests.Remove(guest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //GET: Guests/Import
        public ActionResult Import()
        {
            return View();
        }


        //POST: Guests/Import
        [HttpPost]
        public ActionResult Import(HttpPostedFileBase csvFile)
        {
            var filePath = string.Empty;
            if (csvFile != null)
            {
                var path = Server.MapPath("/uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(csvFile.FileName);
                csvFile.SaveAs(filePath);

                var guestList = CSVLibraryAK.CSVLibraryAK.Import(filePath, true);
                foreach (var guestcsv in guestList.Rows)
                {
                    var guestData = ((DataRow)guestcsv);
                    var guest = new Guest
                    {
                        Code = guestData[0].ToString(),
                        Name = guestData[1].ToString(),
                        Phone = guestData[2].ToString(),
                        IsVip = guestData[3].ToString() == "VIP" ? true : false
                   };

                    db.Guests.Add(guest);
                    

                }
                db.SaveChanges();
            }
            return View();
        }
    }
}
