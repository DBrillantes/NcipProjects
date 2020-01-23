using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCIP.Models
{
    public class Meeting
    {
        public Meeting()
        {
            this.Attendances = new HashSet<Attendance>();
        }
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Meeting Time")]
        [DataType(DataType.DateTime), Required]
        public DateTime DateHeld { get; set; }
        [Required]
        [Display(Name = "Project")]
        public int ProjectID { get; set; }
        [Display(Name = "Stage")]
        public int? StageID { get; set; }

        public virtual Project Project { get; set; }
        public virtual Stage Stage { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}