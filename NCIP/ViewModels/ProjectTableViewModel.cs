using NCIP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCIP.ViewModels
{
    public class ProjectTableViewModel
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Project Title")]
        public string projectTitle { get; set; }

        public Company Company { get; set; }

        public Person Person { get; set; }

        [Required]
        public Stage Stage { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string projectType { get; set; }

        [Required]
        [Display(Name = "Refrence ID")]
        public string reference { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Applied")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime timeStamp { get; set; }
    }
}