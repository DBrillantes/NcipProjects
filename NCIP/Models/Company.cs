using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NCIP.Models
{
    public class Company
    {
        public int ID { get; set; }
        [Required]
        [Display(Name="Company Name")]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public string Landline { get; set; }
        public string Mobile { get; set; }
        [Required]
        [Display(Name = "Businesstype Type")]
        public string Businesstype { get; set; }

        public virtual ICollection<Person> Persons { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}