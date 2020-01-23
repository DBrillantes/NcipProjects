using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
 
using NCIP.Models;

namespace NCIP.Controllers
{
    public class AttendanceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Attendance
        public ActionResult Index()
        {
            var attendance = db.Attendance.Include(a => a.Community).Include(a => a.EthnicGroup).Include(a => a.Meeting);
            return View(attendance.ToList());
        }

        // GET: Attendance/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendance.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // GET: Attendance/Create
        public ActionResult Create()
        {
            ViewBag.CommunityID = new SelectList(db.Communities, "ID", "Sitio");
            ViewBag.EthnicGroupID = new SelectList(db.EthnicGroups, "ID", "Name");
            ViewBag.MeetingID = new SelectList(db.Meetings, "ID", "Name");
            return View();
        }

        // POST: Attendance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Lastname,Firstname,MeetingID,CommunityID,EthnicGroupID")] Attendance attendance, string returnURL)
        {
            if (ModelState.IsValid)
            {
                db.Attendance.Add(attendance);
                db.SaveChanges();                

                if (!String.IsNullOrEmpty(returnURL))
                {
                    return Redirect(returnURL);
                }
                else
                {
                    returnURL = Request.Form["returnURL"];
                    return Redirect(returnURL);
                }

                //return RedirectToAction("Details", "Meetin", new { id = attendance.MeetingID });
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: Attendance/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendance.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            ViewBag.CommunityID = new SelectList(db.Communities, "ID", "Sitio", attendance.CommunityID);
            ViewBag.EthnicGroupID = new SelectList(db.EthnicGroups, "ID", "Name", attendance.EthnicGroupID);
            ViewBag.MeetingID = new SelectList(db.Meetings, "ID", "Name", attendance.MeetingID);
            return View(attendance);
        }

        // POST: Attendance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Lastname,Firstname,MeetingID,CommunityID,EthnicGroupID")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CommunityID = new SelectList(db.Communities, "ID", "Sitio", attendance.CommunityID);
            ViewBag.EthnicGroupID = new SelectList(db.EthnicGroups, "ID", "Name", attendance.EthnicGroupID);
            ViewBag.MeetingID = new SelectList(db.Meetings, "ID", "Name", attendance.MeetingID);
            return View(attendance);
        }

        // GET: Attendance/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendance.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // POST: Attendance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attendance attendance = db.Attendance.Find(id);
            db.Attendance.Remove(attendance);
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
