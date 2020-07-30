using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WicSchool.Data.Models
{
    public class Accountant
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string CPANumber { get; set; }
    }
}
