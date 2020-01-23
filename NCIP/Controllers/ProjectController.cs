using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Diagnostics;
using NCIP.Models;
using NCIP.ViewModels;
using PagedList;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;

namespace NCIP.Controllers
{
    public class ProjectController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Project/
        [AllowAnonymous]

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            ViewBag.SortParamProjectTitle = sortOrder == "projectTitle" ? "projectTitleDsc" : "projectTitle";

            ViewBag.SortParamCompanyName = sortOrder == "CompanyName" ? "CompanyNameDsc" : "";

            ViewBag.SortParamLead = sortOrder == "Lead" ? "LeadDsc" : "";

            ViewBag.SortParamStageName = sortOrder == "StageName" ? "StageNameDsc" : "";

            ViewBag.SortParamProjectType = sortOrder == "ProjectType" ? "ProjectTypeDsc" : "";

            ViewBag.SortParamRefrence = sortOrder == "Refrence" ? "RefrenceDsc" : "";

            ViewBag.SortParamTimeStamp = sortOrder == "timeStamp" ? "timeStampDsc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;



            IEnumerable<ProjectTableViewModel> projects = from tableData in db.Projects.ProjectTo<ProjectTableViewModel>()
                           select tableData;

            if (!String.IsNullOrEmpty(searchString))
            {
                projects = projects.Where(
                    tableData => tableData.projectTitle.Contains(searchString) ||
                    tableData.Company.Name.Contains(searchString) ||
                    tableData.Person.FullName.Contains(searchString) ||
                    tableData.Stage.StageName.Contains(searchString) ||
                    tableData.projectType.Contains(searchString) ||
                    tableData.reference.Contains(searchString)
                    );
            }

            switch (sortOrder)
            {
                case "projectTitle":
                    projects = projects.OrderBy(tableData => tableData.projectTitle);
                    break;
                case "projectTitleDsc":
                    projects = projects.OrderByDescending(tableData => tableData.projectTitle);
                    break;

                case "CompanyName":
                    projects = projects.OrderBy(tableData => tableData.Company.Name);
                    break;
                case "CompanyNameDsc":
                    projects = projects.OrderByDescending(tableData => tableData.Company.Name);
                    break;

                case "Lead":
                    projects = projects.OrderBy(tableData => tableData.Person.Lastname);
                    break;
                case "LeadDsc":
                    projects = projects.OrderByDescending(tableData => tableData.Person.Lastname);
                    break;

                case "StageName":
                    projects = projects.OrderBy(tableData => tableData.Stage.StageName);
                    break;
                case "StageNameDsc":
                    projects = projects.OrderByDescending(tableData => tableData.Stage.StageName);
                    break;

                case "projectType":
                    projects = projects.OrderBy(tableData => tableData.projectTitle);
                    break;
                case "projectTypeDsc":
                    projects = projects.OrderByDescending(tableData => tableData.projectTitle);
                    break;

                case "reference":
                    projects = projects.OrderBy(tableData => tableData.reference);
                    break;
                case "referenceDsc":
                    projects = projects.OrderByDescending(tableData => tableData.reference);
                    break;

                case "timeStamp":
                    projects = projects.OrderBy(tableData => tableData.timeStamp);
                    break;
                case "timeStampDsc":
                    projects = projects.OrderByDescending(tableData => tableData.timeStamp);
                    break;

                default:
                    projects = projects.OrderBy(tableData => tableData.ID);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(projects.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Project/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectDetailsViewModel project = db.Projects
                .ProjectTo<ProjectDetailsViewModel>()
                .SingleOrDefault(p=>p.ID==id);
            project.AffectedCommunities = db.AffectedCommunities
                .Where(affectedCommunities => affectedCommunities.ProjectID == id)
                .ProjectTo<AffectedCommunityTableViewModel>()
                .ToList();

            project.Meetings = db.Meetings
                .Where(meetings => meetings.ProjectID == id)
                .ProjectTo<MeetingTableViewModel>()
                .ToList();
            project.Projectfiles=db.Projectfiles
                .Where(projectfiles => projectfiles.ProjectID == id)
                .ProjectTo<ProjecfileTableViewModel>()
                .ToList();

            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.StageID = new SelectList(db.Stages, "ID", "StageName");
            ViewBag.CommunityID = new SelectList(db.Communities, "ID", "Sitio");
            ViewBag.BarangayID = new SelectList(db.Barangays, "ID", "BarangayName");
            return View(project);
            
        }

        // GET: /Project/Create
        public ActionResult Create()
        {
            ViewBag.CompanyID = new SelectList(db.Companies, "ID", "Name");
            ViewBag.PersonID = new SelectList(db.Persons, "ID", "FullName");
            ViewBag.StageID = new SelectList(db.Stages, "ID", "StageName");
            return View();
        }

        // POST: /Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="projectTitle,projectType,reference,timeStamp,StageID,CompanyID,PersonID")] Project project)
        {
                if (ModelState.IsValid)
                {
                    LoggedActionController.Log("Created Project"+project.projectTitle+"- "+ project.ID, User.Identity.GetUserId());
                    db.Projects.Add(project);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            return View(project);
        }

        // GET: /Project/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectEditViewModel project = db.Projects
                .ProjectTo<ProjectEditViewModel>()
                .SingleOrDefault(p => p.ID == id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyID = new SelectList(db.Companies, "ID", "Name", project.CompanyID);
            ViewBag.PersonID = new SelectList(db.Persons, "ID", "FullName", project.PersonID);
            ViewBag.StageID = new SelectList(db.Stages, "ID", "StageName", project.StageID);
            return View(project);
        }

        // POST: /Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,projectTitle,projectType,reference,timeStamp,StageID,CompanyID,PersonID")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details","Project",new { id= project.ID});
            }
            ViewBag.CompanyID = new SelectList(db.Companies, "ID", "Name", project.CompanyID);
            ViewBag.PersonID = new SelectList(db.Persons, "ID", "Lastname", project.PersonID);
            ViewBag.StageID = new SelectList(db.Stages, "ID", "StageName", project.StageID);
            return View(project);
        }

        // GET: /Project/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: /Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            List < Projectfile > Projectfiles = db.Projectfiles.Where(pf => pf.ProjectID == id).ToList();
            foreach (Projectfile Projectfile in Projectfiles)
            {
                db.Projectfiles.Remove(Projectfile);
            }
            List<AffectedCommunity> AffectedCommunities = db.AffectedCommunities.Where(ac => ac.ProjectID == id).ToList();
            foreach (AffectedCommunity AffectedCommunity in AffectedCommunities)
            {
                db.AffectedCommunities.Remove(AffectedCommunity);
            }
            List < Meeting > Meetings = db.Meetings.Where(m => m.ProjectID == id).ToList();
            foreach (Meeting Meeting in Meetings)
            {
                db.Meetings.Remove(Meeting);
            }

            db.Projects.Remove(project);
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
