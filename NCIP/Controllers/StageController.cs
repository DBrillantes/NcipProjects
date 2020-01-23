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
    public class StageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Stage/
        [AllowAnonymous]
        public ActionResult Index()
        {
            var stages = db.Stages
                .ProjectTo<StageViewModel>()
                .ToList();

            if (User.IsInRole(RoleNames.admin)) {
                return View("AdminIndex", stages);
            }
            return View(stages);
        }

        // GET: /Stage/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var stage = db.Stages
                .ProjectTo<StageViewModel>()
                .SingleOrDefault(q => q.ID == id);
            
            if (stage == null)
            {
                return HttpNotFound();
            }
            ViewBag.showEditDelete = false;
            if (User.IsInRole(RoleNames.admin))
            {
                ViewBag.showEditDelete = true;
            }
            return View(stage);
        }

        // GET: /Stage/Create
        [Authorize(Roles = RoleNames.admin)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Stage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.admin)]
        public ActionResult Create([Bind(Include="ID,StageName")] Stage stage)
        {
            if (ModelState.IsValid)
            {
                db.Stages.Add(stage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stage);
        }

        // GET: /Stage/Edit/5
        [Authorize(Roles = RoleNames.admin)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StageViewModel stage = db.Stages
                 .ProjectTo<StageViewModel>()
                 .SingleOrDefault(q => q.ID == id);
            if (stage == null)
            {
                return HttpNotFound();
            }
            return View(stage);
        }

        // POST: /Stage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.admin)]
        public ActionResult Edit([Bind(Include="ID,StageName")] Stage stageVM)
        {

            if (ModelState.IsValid)
            {
                var stage = AutoMapper.Mapper.Map<Stage, Stage>(stageVM);
                db.Entry(stage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stageVM);
        }

        // GET: /Stage/Delete/5
        [Authorize(Roles = RoleNames.admin)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            StageViewModel stage = db.Stages
                .ProjectTo<StageViewModel>()
                .SingleOrDefault(q => q.ID == id);

            if (stage == null)
            {
                return HttpNotFound();
            }
            return View(stage);
        }

        // POST: /Stage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.admin)]
        public ActionResult DeleteConfirmed(int id)
        {
            Stage stage = db.Stages.Find(id);
            stage.Projects = db.Projects
                .Where(q => q.StageID == stage.ID)
                .ToList();
            //needs improvement
            foreach (Project project in stage.Projects)
            {
                project.StageID = 1;
            }
            db.Stages.Remove(stage);
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
