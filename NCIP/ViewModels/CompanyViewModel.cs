using NCIP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCIP.ViewModels
{
    public class CompanyViewModel
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Company Name")]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public string Landline { get; set; }
        public string Mobile { get; set; }
        [Required]
        [Display(Name = "Businesstype Type")]
        public string Businesstype { get; set; }

        public virtual ICollection<Person> Persons { get; set; }
        public virtual ICollection<ProjectTableViewModel> Projects { get; set; }
    }
}