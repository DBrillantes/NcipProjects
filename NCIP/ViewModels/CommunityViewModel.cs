using NCIP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCIP.ViewModels
{
    public class CommunityViewModel
    {
        //public int ID { get; set; }
        [Required]
        public string Sitio { get; set; }
        //public int Population { get; set; }
        [Required]
        public string Representative { get; set; }
        //[Display(Name = "Barangay")]
        //public int BarangayID { get; set; }
        //public int AttendanceID { get; set; }

        public virtual Barangay Barangay { get; set; }
    }
}