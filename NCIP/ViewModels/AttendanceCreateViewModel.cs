using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCIP.ViewModels
{
    public class AttendanceCreateViewModel
    {
        [Required]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Meeting Attended")]
        public int MeetingID { get; set; }

        [Display(Name = "Community")]
        public int CommunityID { get; set; }

        [Display(Name = "Ethnicity")]
        public int? EthnicGroupID { get; set; }
    }
}