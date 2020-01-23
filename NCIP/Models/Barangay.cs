using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NCIP.Models
{
    public class Barangay
    {
        public Barangay() { 
            this.Communities = new HashSet<Community>();
        }
        public int ID { get; set; }
        [Required]
        [Display(Name = "Barangay")]
        public string BarangayName { get; set; }
        [Required]
        public string Classification { get; set; }

        public virtual ICollection<Community> Communities { get; set; }
    }
}