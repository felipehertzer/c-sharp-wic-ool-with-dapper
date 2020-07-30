using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WicSchool.Data.Models
{
    public class Technician
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string ACSNumber { get; set; }
        public DateTime Expire { get; set; }
    }
}
