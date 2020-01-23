using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NCIP.Models
{
    public class Stage
    {
        public int ID { get; set; }
        [Required]
        [Display(Name="Status")]
        public string StageName { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}