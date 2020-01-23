using NCIP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCIP.ViewModels
{
    public class MeetingTableViewModel
    {
        public string Name { get; set; }

        public int ID { get; set; }
        [Required]
        [Display(Name = "Meeting Time and Date")]
        public DateTime DateHeld { get; set; }

        [Display(Name = "Stage")]
        public int? StageID { get; set; }

        public virtual Stage Stage { get; set; }
    }
}