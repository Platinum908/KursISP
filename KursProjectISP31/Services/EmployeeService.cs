using KursProjectISP31.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursProjectISP31.Services
{
    public class EmployeeService : BaseService<Employee>
    {
        public override bool Add(Employee obj)
        {
            bool IsAdded = false;
            if (obj.Age < 18 || obj.Age > 70)
                throw new ArgumentException("Не соблюден лимит по возрасту");
            using (objSqlconnection) 
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_InsertEmployee";
                objSqlCommand.Parameters.AddWithValue("@NameEmp",obj.NameEmp);
                objSqlCommand.Parameters.AddWithValue("@Age", obj.Age);
                objSqlconnection.Open();
                int addRows = objSqlCommand.ExecuteNonQuery();
                IsAdded = addRows > 0;
            }
            return IsAdded;
        }
        public override bool Delete(int id)
        {
            bool IsDeleted = false;
            using (objSqlconnection)
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_DeleteEmployee";
                objSqlCommand.Parameters.AddWithValue("@Id", id);
                objSqlconnection.Open();
                int delRows = objSqlCommand.ExecuteNonQuery();
                IsDeleted = delRows > 0;
            }
            return IsDeleted;
        }
        public override List<Employee> GetAll()
        {
            
        }

        public override bool Update(Employee obj)
        {
            throw new NotImplementedException();
        }
    }
}
