using Microsoft.AspNet.Identity;
using NCIP.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NCIP.Models
{
    public class Projectfile
    {
        public int ID { get; set; }
        public string Filename { get; set; }
        public string Filelocation { get; set; }
        public int ProjectID { get; set; }
        public string ApplicationUserID { get; set; }

        public virtual Project Project { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}