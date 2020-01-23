using NCIP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIP.ViewModels
{
    public class ProjecfileTableViewModel
    {
        public int ID { get; set; }
        public int ProjectID { get; set; }
        public string Filename { get; set; }
        public string Filelocation { get; set; }
        public virtual Project Project { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual string ApplicationUserName
        {
            get
            {
                return ApplicationUser.FirstName + ", " + ApplicationUser.LastName;
            }
        }
    }
}