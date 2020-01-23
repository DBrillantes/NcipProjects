using NCIP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCIP.ViewModels
{
    public class AffectedCommunityTableViewModel
    {
        public int ProjectID { get; set; }
        [Required]
        [Display(Name = "Community")]
        public CommunityViewModel Community { get; set; }

    }
}