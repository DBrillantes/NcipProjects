using NCIP.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NCIP.ViewModels
{
    public class StageViewModel
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Status")]
        public string StageName { get; set; }

        public virtual ICollection<ProjectTableViewModel> Projects { get; set; }
    }
}