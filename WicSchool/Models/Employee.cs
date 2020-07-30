using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WicSchool.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Display(Name = "Employee ID")]
        [Range(100000, 999999, ErrorMessage = "You need to enter a valid EmployeeId")]
        public int EmployeeId { get; set; }
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "You need to give us your full name.")]
        public string EmployeeName { get; set; }
        [Display(Name = "Type of Employee")]
        [Required(ErrorMessage = "You need to give us what kind of employee you are.")]
        public string EmployeeType { get; set; }
        [Display(Name = "Rate per Hour")]
        [Required(ErrorMessage = "You need to give us your hourly rate.")]
        public double RatePerHour { get; set; }
        public Accountant Accountant { get; set; }
        public Technician Technician { get; set; }

        public double CalculateWage(double hours)
        {
            return this.RatePerHour * hours;
        }

        public double CalculateTax(double hours)
        {
            return (this.CalculateWage(hours) * double.Parse("19.20")) / 100;
        }
    }
}