using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NCIP.Models
{
    public class Person
    {
        public int ID { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Address { get; set; }
        public string landline { get; set; }
        public string Mobile { get; set; }
        public int? CompanyID { get; set; }

        [Display(Name="Lead")]
        public string FullName
        {
            get {
                return Lastname + ", " + Firstname;
            }
        }
        public virtual Company Company { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}