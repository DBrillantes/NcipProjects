using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NCIP.Models
{
    public class Community
    {
        public int ID { get; set; }
        [Required]
        public string Sitio { get; set; }
        public int Population { get; set; }
        [Required]
        public string Representative { get; set; }
        [Required]
        [Display(Name="Barangay")]
        public int BarangayID { get; set; }
        //public int AttendanceID { get; set; }

        public virtual Barangay Barangay { get; set; }
    }
}