using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WicSchool.Data.DataAccess;
using WicSchool.Data.Models;

namespace WicSchool.Data.BusinessLogic
{
    public static class EmployeeProcessor
    {
        public static int CreateEmployee(int employeeId, string employeeName,
            string employeeType, double ratePerHour)
        {
            Employee data = new Employee
            {
                EmployeeId = employeeId,
                EmployeeName = employeeName,
                EmployeeType = employeeType,
                RatePerHour = ratePerHour
            };

            string sql = @"insert into dbo.Employees (EmployeeId, EmployeeName, EmployeeType, RatePerHour)
                           values (@EmployeeId, @EmployeeName, @EmployeeType, @RatePerHour); SELECT CAST(SCOPE_IDENTITY() as int)";

            return SQLDataAccess.InsertData(sql, data);
        }

        public static int EditEmployee(int employeeId, string employeeName,
            string employeeType, double ratePerHour, int id)
        {
            Employee data = new Employee
            {
                Id = id,
                EmployeeId = employeeId,
                EmployeeName = employeeName,
                EmployeeType = employeeType,
                RatePerHour = ratePerHour
            };

            string sql = @"update dbo.Employees set EmployeeId = @EmployeeId, EmployeeName = @EmployeeName, EmployeeType = @EmployeeType, RatePerHour = @RatePerHour WHERE Id = @Id;";

            return SQLDataAccess.SaveData(sql, data);
        }

        public static List<Employee> GetEmployee(int id)
        {

            string sql = @"SELECT * FROM dbo.Employees WHERE Id = " + id + ";";

            return SQLDataAccess.LoadData<Employee>(sql);
        }
        
        public static List<Employee> GetEmployeeByEmployeeID(int id)
        {

            string sql = @"SELECT * FROM dbo.Employees WHERE EmployeeId = '" + id + "';";

            return SQLDataAccess.LoadData<Employee>(sql);
        }

        public static List<Employee> SearchEmployee(string query)
        {

            string sql = @"SELECT * FROM dbo.Employees WHERE EmployeeName LIKE '%" + query + "%';";

            return SQLDataAccess.LoadData<Employee>(sql);
        }

        public static int DeleteEmployee(int id)
        {
            
            string sql = @"delete dbo.Employees WHERE Id = " + id + ";";

            return SQLDataAccess.SaveData(sql, new { id });
        }

        public static List<Employee> LoadEmployees()
        {
            string sql = @"select Id, EmployeeId, EmployeeName, EmployeeType, RatePerHour
                           from dbo.Employees;";

            return SQLDataAccess.LoadData<Employee>(sql);
        }
    }
}
