using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WicSchool.Data.DataAccess;
using WicSchool.Data.Models;

namespace WicSchool.Data.BusinessLogic
{
    public static class TechnicianProcessor
    {
        public static int CreateTechnician(int employeeId, string acsNumber, DateTime expire)
        {
            Technician data = new Technician
            {
                EmployeeId = employeeId,
                ACSNumber = acsNumber,
                Expire = expire,
            };

            string sql = @"insert into dbo.Technicians (EmployeeId, ACSNumber, Expire)
                           values (@EmployeeId, @ACSNumber, @Expire);  SELECT CAST(SCOPE_IDENTITY() as int)";

            return SQLDataAccess.InsertData(sql, data);
        }

        public static int EditTechnician(int employeeId, string acsNumber, DateTime expire)
        {
            Technician data = new Technician
            {
                EmployeeId = employeeId,
                ACSNumber = acsNumber,
                Expire = expire,
            };

            string sql = @"update dbo.Technicians set ACSNumber = @ACSNumber, Expire = @Expire WHERE EmployeeId = @EmployeeId;";

            return SQLDataAccess.SaveData(sql, data);
        }

        public static List<Technician> GetTechnician(int id)
        {
            string sql = @"SELECT * FROM dbo.Technicians WHERE EmployeeId = " + id + ";";

            return SQLDataAccess.LoadData<Technician>(sql);
        }

        public static int DeleteTechnician(int id)
        {

            string sql = @"delete dbo.Technicians WHERE EmployeeId = " + id + ";";

            return SQLDataAccess.SaveData(sql, new { id });
        }
    }
}
