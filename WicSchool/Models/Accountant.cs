using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WicSchool.Models
{
    public class Accountant
    {
        [Display(Name = "CPA Number")]
        public string CPANumber { get; set; }
    }
}