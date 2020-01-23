using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using NCIP.Models;
using NCIP.ViewModels;

namespace NCIP.Controllers
{
    public class MeetingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Meeting
        [AllowAnonymous]
        public ActionResult Index()
        {
            var meetings = db.Meetings.Include(m => m.Project).Include(m => m.Stage);
            return View(meetings.ToList());
        }

        // GET: Meeting/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingDetailsViewModel meeting = db.Meetings
                .ProjectTo<MeetingDetailsViewModel>()
                .SingleOrDefault(m => m.ID == id);
            meeting.Attendances = db.Attendance.Where(a => a.MeetingID == id).ProjectTo<AtendanceTableViewModel>().ToList();
            if (meeting == null)
            {
                return HttpNotFound();
            }
            ViewBag.CommunityID = new SelectList(db.Communities, "ID", "Sitio");
            ViewBag.EthnicGroupID = new SelectList(db.EthnicGroups, "ID", "Name");
            ViewBag.MeetingID = new SelectList(db.Meetings, "ID", "Name");
            return View(meeting);
        }

        // GET: Meeting/Create
        public ActionResult Create()
        {
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "projectTitle");
            ViewBag.StageID = new SelectList(db.Stages, "ID", "StageName");
            return View();
        }

        // POST: Meeting/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,DateHeld,ProjectID,StageID")] MeetingCreationViewModel meeting, string returnURL)
        {
            if (ModelState.IsValid)
            {
                var meetingToSave = AutoMapper.Mapper.Map<MeetingCreationViewModel, Meeting>(meeting);
                db.Meetings.Add(meetingToSave);
                db.SaveChanges();
                if (!String.IsNullOrEmpty(returnURL))
                {
                    return Redirect(returnURL);
                }
                else {
                    returnURL = Request.Form["returnURL"];
                    return Redirect(returnURL);
                }
                //return RedirectToAction("Details", "Project", new { id = meeting.ProjectID });
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: Meeting/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "projectTitle", meeting.ProjectID);
            ViewBag.StageID = new SelectList(db.Stages, "ID", "StageName", meeting.StageID);
            return View(meeting);
        }

        // POST: Meeting/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,DateHeld,ProjectID,StageID")] Meeting meeting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meeting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "projectTitle", meeting.ProjectID);
            ViewBag.StageID = new SelectList(db.Stages, "ID", "StageName", meeting.StageID);
            return View(meeting);
        }

        // GET: Meeting/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            return View(meeting);
        }

        // POST: Meeting/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meeting meeting = db.Meetings.Find(id);
            //needs improvement
            List<Attendance> toDelete = db.Attendance.Where(a => a.MeetingID == id).ToList();
            foreach (Attendance atendee in toDelete) {
                db.Attendance.Remove(atendee);
            }
            db.Meetings.Remove(meeting);
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
