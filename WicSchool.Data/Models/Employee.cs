using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WicSchool.Data.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeType { get; set; }
        public double RatePerHour { get; set; }
        public Accountant Accountant { get; set; }
        public Technician Technician { get; set; }

    }
}
