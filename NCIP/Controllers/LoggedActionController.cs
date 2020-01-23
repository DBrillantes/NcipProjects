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
    public class LoggedActionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LoggedAction
        public ActionResult log()
        {
            var loggedActions = db.LoggedActions.Include(l => l.ApplicationUser).ToList();

            return View(loggedActions.ToList());
        }

        public static void Log(string actionDone, string applicationUserID) {
            LoggedAction action = new LoggedAction { ActionDone = actionDone, ApplicationUserID = applicationUserID };
            LoggedActionController newThing = new LoggedActionController();
            newThing.LogToDB(action);
        }
        private void LogToDB(LoggedAction action) {
            db.LoggedActions.Add(action);
            db.SaveChanges();
        }
        // GET: LoggedAction/Details/5
 

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
