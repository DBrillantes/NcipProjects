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
    public class BarangayController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Barangay/
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Barangays.ToList());
        }

        // GET: /Barangay/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Barangay barangay = db.Barangays
                .Include(b=>b.Communities)
                .SingleOrDefault(b=>b.ID==id);
            if (barangay == null)
            {
                return HttpNotFound();
            }
            return View(barangay);
        }

        // GET: /Barangay/Create
        [Authorize(Roles = RoleNames.admin)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Barangay/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.admin)]
        public ActionResult Create([Bind(Include="BarangayID,BarangayName,Classification")] Barangay barangay)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Barangays.Add(barangay);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }catch(DataException){
                ModelState.AddModelError("", "Cannot commit changes");
            }

            return View(barangay);
        }

        // GET: /Barangay/Edit/5
        [Authorize(Roles = RoleNames.admin)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Barangay barangay = db.Barangays.Find(id);
            if (barangay == null)
            {
                return HttpNotFound();
            }
            return View(barangay);
        }

        // POST: /Barangay/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = RoleNames.admin)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="BarangayID,BarangayName,Classification")] Barangay barangay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(barangay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(barangay);
        }

        // GET: /Barangay/Delete/5
        [Authorize(Roles = RoleNames.admin)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Barangay barangay = db.Barangays.Find(id);
            if (barangay == null)
            {
                return HttpNotFound();
            }
            return View(barangay);
        }

        // POST: /Barangay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.admin)]
        public ActionResult DeleteConfirmed(int id)
        {
            Barangay barangay = db.Barangays.Find(id);
            db.Barangays.Remove(barangay);
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
