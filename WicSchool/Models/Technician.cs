using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WicSchool.Models
{
    public class Technician
    {
        [Display(Name = "ACS Number")]
        public string ACSNumber { get; set; }
        [Display(Name = "Expire Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Expire { get; set; }
    }
}