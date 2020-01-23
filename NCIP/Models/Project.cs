using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NCIP.Models
{
    public class Project
    {
        public Project() {
            this.AffectedCommunities = new HashSet<AffectedCommunity>();
            this.Projectfiles = new HashSet<Projectfile>();
        }

        public int ID { get; set; }

        [Required]
        [Display(Name="Project Title")]
        public string projectTitle { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string projectType { get; set; }

        [Required]
        [Display(Name = "Refrence ID")]
        public string reference { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date), Required]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode =true)]
        public DateTime timeStamp { get; set; }

        [Required]
        [Display(Name = "Current Stage")]
        public int StageID { get; set; }

        [Required]
        [Display(Name = "Company")]
        public int? CompanyID { get; set; }

        [Required]
        [Display(Name = "Lead")]
        public int PersonID { get; set; }

        public string FileDirectory { get; set; }

        public virtual Company Company { get; set; }
        public virtual Person Person{get;set;}
        public virtual Stage Stage { get; set; }
        public virtual ICollection<Projectfile> Projectfiles { get; set; }
        public virtual ICollection<AffectedCommunity> AffectedCommunities { get; set; }
        public virtual ICollection<Meeting> Meetings { get; set; }
    }
}