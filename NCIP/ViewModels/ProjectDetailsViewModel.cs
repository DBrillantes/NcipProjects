using NCIP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCIP.ViewModels
{
    public class ProjectDetailsViewModel
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Project Title")]
        public string projectTitle { get; set; }

        public virtual Person Person { get; set; }

        public virtual Stage Stage { get; set; }

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

        public virtual IEnumerable<AffectedCommunityTableViewModel> AffectedCommunities { get; set; }
        public virtual IEnumerable<ProjecfileTableViewModel> Projectfiles { get; set; }

        public string FileDirectory { get; set; }
        public virtual Company Company { get; set; }
        public virtual IEnumerable<MeetingTableViewModel> Meetings { get; set; }
    }
}