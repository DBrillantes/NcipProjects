using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NCIP.Models;

using System.IO;
using System.Diagnostics;
using Microsoft.AspNet.Identity;
using NCIP.ViewModels;

namespace NCIP.Controllers
{
    public class ProjectfileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Projectfile/
        public ActionResult Index()
        {
            var projectfiles = db.Projectfiles.Include(p => p.Project);
            return View(projectfiles.ToList());
        }

        // GET: /Projectfile/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projectfile projectfiles = db.Projectfiles.Find(id);
            if (projectfiles == null)
            {
                return HttpNotFound();
            }
            return View(projectfiles);
        }

        // GET: /Projectfile/Create
        public ActionResult Create()
        {
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "projectTitle");
            return View();
        }

        // POST: /Projectfile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjecfileTableViewModel projectfilesVM, HttpPostedFileBase file)
        {
            var projectfiles = AutoMapper.Mapper.Map<ProjecfileTableViewModel, Projectfile>(projectfilesVM);
            try
            {
                
                if (ModelState.IsValid)
                {

                    if (file!=null&&file.ContentLength > 0)
                    {
                        
                        string mimeType = MimeMapping.GetMimeMapping(file.FileName);
                        var project = db.Projects.Include(p => p.Company)
                            .SingleOrDefault(p => p.ID == projectfiles.ProjectID);
                        if (project.FileDirectory == null)
                        {
                            project.FileDirectory = "~/App_Data/upload/" 
                                + project.Company.Name + "/" + project.projectTitle;
                            db.SaveChanges();
                        }
                        string baseFileName = Path.GetFileNameWithoutExtension(file.FileName);
                        string fileName = baseFileName;

                        string path = Server.MapPath(project.FileDirectory + "/" + mimeType + "/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string extension = Path.GetExtension(file.FileName);

                        projectfiles.Filelocation = path;

                        projectfiles.Filename = fileName + extension;
                        fileName = fileName + extension;

                        if (System.IO.File.Exists(path + fileName))
                        {
                            projectfiles.Filename = baseFileName + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + extension;
                            fileName = baseFileName + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + extension;

                        }
                        projectfiles.ApplicationUserID = User.Identity.GetUserId();
                        db.Projectfiles.Add(projectfiles);
                        db.SaveChanges();
                        file.SaveAs(Path.Combine(path, fileName));
                    }
                    else {
                        return RedirectToAction("Details", "Project", new { ID = projectfiles.ProjectID });
                    }
                    
                }
            }catch(DataException e){
                ModelState.AddModelError("","UploadFailed "+e.Message);
            }
            return RedirectToAction("Details", "Project", new { ID = projectfiles.ProjectID });
        }

        public ActionResult DownloadFile(int pfileID)
        {
            Projectfile pFiles = db.Projectfiles.Find(pfileID);

            //string prefix = "Project - " + pFiles.ProjectID + " - ";

            string path = pFiles.Filelocation;
            string fileName = pFiles.Filename;
            if (!System.IO.File.Exists(path + fileName))
            {
                return HttpNotFound();
            }
            byte[] filebyte = System.IO.File.ReadAllBytes(path + fileName);
            
            return File(filebyte, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);


        }

        // GET: /Projectfile/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projectfile projectfiles = db.Projectfiles.Find(id);
            if (projectfiles == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "projectTitle", projectfiles.ProjectID);
            return View(projectfiles);
        }

        // POST: /Projectfile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Filename,Filelocation,ProjectID")] Projectfile projectfiles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectfiles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "projectTitle", projectfiles.ProjectID);
            return View(projectfiles);
        }

        // GET: /Projectfile/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projectfile projectfiles = db.Projectfiles
                .Include(pf=>pf.Project)
                .SingleOrDefault(pf => pf.ID == id);
            if (projectfiles == null)
            {
                return HttpNotFound();
            }
            return View(projectfiles);
        }

        // POST: /Projectfile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Projectfile projectfiles = db.Projectfiles.Find(id);
            //FileInfo file = new FileInfo(Path.Combine(projectfiles.Filelocation, projectfiles.Filename));
            db.Projectfiles.Remove(projectfiles);
            db.SaveChanges();
            //file.Delete();
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
