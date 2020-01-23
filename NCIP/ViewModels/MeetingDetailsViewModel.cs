using NCIP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCIP.ViewModels
{
    public class MeetingDetailsViewModel
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Meeting Time")]
        public DateTime DateHeld { get; set; }
        [Required]
        [Display(Name = "Project")]
        public int ProjectID { get; set; }
        [Display(Name = "Stage")]
        public int? StageID { get; set; }

        public virtual Project Project { get; set; }
        public virtual Stage Stage { get; set; }
        public virtual IEnumerable<AtendanceTableViewModel> Attendances { get; set; }
    }
}