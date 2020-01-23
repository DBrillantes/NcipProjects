using NCIP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCIP.ViewModels
{
    public class AtendanceTableViewModel
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string Firstname { get; set; }

        [Display(Name = "Community")]
        public int CommunityID { get; set; }

        [Display(Name = "Ethnicity")]
        public int? EthnicGroupID { get; set; }

        public virtual Community Community { get; set; }
        public virtual EthnicGroup EthnicGroup { get; set; }
    }
}