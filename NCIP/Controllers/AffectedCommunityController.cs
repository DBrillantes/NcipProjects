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
    public class AffectedCommunityController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AffectedCommunity
        public ActionResult Index()
        {
            var affectedCommunities = db.AffectedCommunities.Include(c => c.Project).Include(c=>c.Community).ToList();
            return View(affectedCommunities);
        }

        // GET: AffectedCommunity/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AffectedCommunity affectedCommunity = db.AffectedCommunities.Find(id);
            if (affectedCommunity == null)
            {
                return HttpNotFound();
            }
            return View(affectedCommunity);
        }

        // GET: AffectedCommunity/Create
        public ActionResult Create()
        {
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "projectTitle");
            ViewBag.CommunityID = new SelectList(db.Communities, "ID", "Sitio");
            return View();
        }

        // POST: AffectedCommunity/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProjectID,CommunityID")] AffectedCommunity affectedCommunity)
        {
            if (ModelState.IsValid)
            {
                //testing uniqueness
                var testUnique = db.AffectedCommunities.Where(ac => ac.ProjectID == affectedCommunity.ProjectID).SingleOrDefault(ac=>ac.CommunityID== affectedCommunity.CommunityID);
                if (testUnique == null) {
                    db.AffectedCommunities.Add(affectedCommunity);
                    db.SaveChanges();
                    string returnURL = Request.Form["returnURL"];

                    if (!String.IsNullOrEmpty(returnURL))
                    {
                        return Redirect(returnURL);
                    }

                }
                return RedirectToAction("Details", "Project", new { id = affectedCommunity.ProjectID });
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: AffectedCommunity/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AffectedCommunity affectedCommunity = db.AffectedCommunities.Find(id);
            if (affectedCommunity == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "projectTitle");
            ViewBag.CommunityID = new SelectList(db.Communities, "ID", "Sitio");
            return View(affectedCommunity);
        }

        // POST: AffectedCommunity/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProjectID,CommunityID")] AffectedCommunity affectedCommunity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(affectedCommunity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(affectedCommunity);
        }

        // GET: AffectedCommunity/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AffectedCommunity affectedCommunity = db.AffectedCommunities.Find(id);
            if (affectedCommunity == null)
            {
                return HttpNotFound();
            }
            return View(affectedCommunity);
        }

        // POST: AffectedCommunity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AffectedCommunity affectedCommunity = db.AffectedCommunities.Find(id);
            db.AffectedCommunities.Remove(affectedCommunity);
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
