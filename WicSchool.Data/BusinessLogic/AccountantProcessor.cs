using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WicSchool.Data.DataAccess;
using WicSchool.Data.Models;

namespace WicSchool.Data.BusinessLogic
{
    public static class AccountantProcessor
    {
        public static int CreateAccountant(int employeeId, string cpaNumber)
        {
            Accountant data = new Accountant
            {
                EmployeeId = employeeId,
                CPANumber = cpaNumber
            };

            string sql = @"insert into dbo.Accountants (EmployeeId, CPANumber)
                           values (@EmployeeId, @CPANumber); SELECT CAST(SCOPE_IDENTITY() as int)";

            return SQLDataAccess.InsertData(sql, data);
        }

        public static int EditAccountant(int employeeId, string cpaNumber)
        {
            Accountant data = new Accountant
            {
                EmployeeId = employeeId,
                CPANumber = cpaNumber
            };

            string sql = @"update dbo.Accountants set CPANumber = @CPANumber WHERE EmployeeId = @EmployeeId;";

            return SQLDataAccess.SaveData(sql, data);
        }

        public static List<Accountant> GetAccountant(int id)
        {
            string sql = @"SELECT * FROM dbo.Accountants WHERE EmployeeId = " + id + ";";

            return SQLDataAccess.LoadData<Accountant>(sql);
        }

        public static int DeleteAccountant(int id)
        {

            string sql = @"delete dbo.Accountants WHERE EmployeeId = " + id + ";";

            return SQLDataAccess.SaveData(sql, new { id });
        }
    }
}
