using NCIP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCIP.ViewModels
{
    public class ProjectEditViewModel
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Project Title")]
        public string projectTitle { get; set; }

        [Required]
        [Display(Name = "Company")]
        public int CompanyID { get; set; }

        [Required]
        [Display(Name = "Lead")]
        public int PersonID { get; set; }

        [Required]
        [Display(Name = "Status")]
        public int StageID { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string projectType { get; set; }

        [Required]
        [Display(Name = "Refrence ID")]
        public string reference { get; set; }

        [Display(Name = "Date Applied")]
        [DataType(DataType.Date), Required]
        [DisplayFormat(DataFormatString = "{0:MMM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime timeStamp { get; set; }        

        public string FileDirectory { get; set; }

        public virtual Company Company { get; set; }
        public virtual Person Person { get; set; }
        public virtual Stage Stage { get; set; }
    }
}