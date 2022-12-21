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
using Pramod.GuestTracker.Web.ViewModels;

namespace Pramod.GuestTracker.Web.Controllers
{
    public class AttendencesController : Controller
    {
        private GuestTrackerContext db = new GuestTrackerContext();

        // GET: Attendences
        public ActionResult Index()
        {
            return View(db.Attendences
                .Include(a=>a.Guest)
                .ToList());
        }

        public ActionResult RegConfirm(RegisterAttendanceViewModel model)
        {
            return View(model);
        }

        // GET: Attendences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Attendence attendence = db.Attendences.Find(id);
            Attendence attendence = db.Attendences.Include(a => a.Guest).FirstOrDefault(a => a.Id == id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            return View(attendence);
        }

        // GET: Attendences/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Attendences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GuestID,ArrivedAt,NumberOfPeople")] Attendence attendence)
        {
            if (ModelState.IsValid)
            {
                db.Attendences.Add(attendence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(attendence);
        }

        // GET: Attendences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendence attendence = db.Attendences.Find(id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            return View(attendence);
        }

        // POST: Attendences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GuestId,ArrivedAt,NumberOfPeople")] Attendence attendence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(attendence);
        }

        // GET: Attendences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendence attendence = db.Attendences.Find(id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            return View(attendence);
        }

        // POST: Attendences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attendence attendence = db.Attendences.Find(id);
            db.Attendences.Remove(attendence);
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

    }
}
