using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NCIP.Models
{
    public class Attendance
    {
        public int ID { get; set; }

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

        public virtual Meeting Meeting { get; set; }
        public virtual Community Community { get; set; }
        public virtual EthnicGroup EthnicGroup { get; set; }
    }
}