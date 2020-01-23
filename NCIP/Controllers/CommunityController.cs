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
    public class CommunityController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Community/
        [AllowAnonymous]
        public ActionResult Index()
        {
            var communities = db.Communities.Include(c => c.Barangay);
            return View(communities.ToList());
        }

        // GET: /Community/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Community community = db.Communities
                .Include(c=>c.Barangay)
                .SingleOrDefault(c=>c.ID==id);
            if (community == null)
            {
                return HttpNotFound();
            }
            return View(community);
        }

        // GET: /Community/Create
        [Authorize(Roles = RoleNames.admin)]
        public ActionResult Create()
        {
            ViewBag.BarangayID = new SelectList(db.Barangays, "ID", "BarangayName");
            return View();
        }

        // POST: /Community/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Sitio,Population,Representative,BarangayID")] Community community, string returnURL)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Communities.Add(community);
                    db.SaveChanges();
                    return Redirect(returnURL);
                }
            }catch(DataException){
                ModelState.AddModelError("", "Cannot commit changes");
            }

            ViewBag.BarangayID = new SelectList(db.Barangays, "BarangayID", "BarangayName", community.BarangayID);
            return View(community);
        }

        // GET: /Community/Edit/5
        [Authorize(Roles = RoleNames.admin)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Community community = db.Communities.SingleOrDefault(c=>c.ID==id);
            if (community == null)
            {
                return HttpNotFound();
            }
            ViewBag.BarangayID = new SelectList(db.Barangays, "ID", "BarangayName");
            return View(community);
        }

        // POST: /Community/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.admin)]
        public ActionResult Edit([Bind(Include="ID,Sitio,Population,Representative,BarangayID")] Community community)
        {
            if (ModelState.IsValid)
            {
                db.Entry(community).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BarangayID = new SelectList(db.Barangays, "BarangayID", "BarangayName", community.BarangayID);
            return View(community);
        }

        // GET: /Community/Delete/5
        [Authorize(Roles = RoleNames.admin)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Community community = db.Communities.Find(id);
            if (community == null)
            {
                return HttpNotFound();
            }
            return View(community);
        }

        // POST: /Community/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.admin)]
        public ActionResult DeleteConfirmed(int id)
        {
            Community community = db.Communities
                .SingleOrDefault(c=>c.ID==id);
            //needs improvement
            List <AffectedCommunity> toDelete = db.AffectedCommunities
                .Where(ac => ac.CommunityID == id)
                .ToList();
            foreach (AffectedCommunity affectedCommunity in toDelete)
            {
                db.AffectedCommunities.Remove(affectedCommunity);
            }
            db.Communities.Remove(community);
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
