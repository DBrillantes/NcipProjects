﻿using NCIP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCIP.ViewModels
{
    public class MeetingCreationViewModel
    {
        [Required]
        public string Name { get; set; }

        [Display(Name = "Meeting Time")]
        [DataType(DataType.DateTime), Required]
        public DateTime DateHeld { get; set; }

        [Required]
        [Display(Name = "Project")]
        public int ProjectID { get; set; }

        [Display(Name = "Stage")]
        public int? StageID { get; set; }

    }
}