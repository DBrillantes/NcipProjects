using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCIP.Models
{
    public class AffectedCommunity
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Project")]
        public int ProjectID { get; set; }
        [Required]
        [Display(Name = "Community")]
        public int CommunityID { get; set; }
        public virtual Community Community { get; set; }
        public virtual Project Project { get; set; }
    }
}