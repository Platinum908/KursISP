using KursProjectISP31.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursProjectISP31.Services
{
    public class EmployeeService : BaseService<Employee>
    {
        public EmployeeService():base()
        {
        }

        public override bool Add(Employee obj)
        {
            bool IsAdded = false;
            if (obj.Age < 18 || obj.Age > 70)
                throw new ArgumentException("Не соблюден лимит по возрасту");
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_InsertEmployee";
                objSqlCommand.Parameters.AddWithValue("@NameEmp", obj.NameEmp);
                objSqlCommand.Parameters.AddWithValue("@Age", obj.Age);
                objSqlconnection.Open();
                int addRows = objSqlCommand.ExecuteNonQuery();
                IsAdded = addRows > 0;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                objSqlconnection.Close();
            }
            return IsAdded;
        }
        public override bool Delete(int id)
        {
            bool IsDeleted = false;
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_DeleteEmployee";
                objSqlCommand.Parameters.AddWithValue("@Id", id);
                objSqlconnection.Open();
                int delRows = objSqlCommand.ExecuteNonQuery();
                IsDeleted = delRows > 0;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                objSqlconnection.Close();
            }
            return IsDeleted;
        }
        public override List<Employee> GetAll()
        {
            List<Employee> list = new List<Employee>();
            try 
            { 
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_SelectAllEmployee";
                objSqlconnection.Open();
                var ObjSqlDataReader = objSqlCommand.ExecuteReader();
                if (ObjSqlDataReader.HasRows)
                {
                    Employee objEmployee = null;
                    while (ObjSqlDataReader.Read())
                    {
                        objEmployee = new Employee();
                        objEmployee.Id = ObjSqlDataReader.GetInt32(0);
                        objEmployee.NameEmp = ObjSqlDataReader.GetString(1);
                        objEmployee.Age = ObjSqlDataReader.GetInt32(2);
                        list.Add(objEmployee);
                    }
                }
                ObjSqlDataReader.Close();
            }
            catch(SqlException ex)
            {
                throw ex;
            }
            finally
            {
                objSqlconnection.Close();
            }
            return list;
        }
        public override bool Update(Employee obj)
        {
            bool IsUpdate = false;
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_UpdateEmployee";
                objSqlCommand.Parameters.AddWithValue("@Id", obj.Id);
                objSqlCommand.Parameters.AddWithValue("@NameEmp", obj.NameEmp);
                objSqlCommand.Parameters.AddWithValue("@Age", obj.Age);
                objSqlconnection.Open();
                int updateRows = objSqlCommand.ExecuteNonQuery();
                IsUpdate = updateRows > 0;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                objSqlconnection.Close();
            }
            return IsUpdate;
        }
    }
}
