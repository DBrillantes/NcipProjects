using NCIP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIP.ViewModels
{
    public class PersonDetailsViewModel
    {
        public int ID { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Address { get; set; }
        public string landline { get; set; }
        public string Mobile { get; set; }
        public string FullName
        {
            get
            {
                return Lastname + ", " + Firstname;
            }
        }
        public virtual Company Company { get; set; }
        public virtual ICollection<ProjectTableViewModel> Projects { get; set; }
    }
}