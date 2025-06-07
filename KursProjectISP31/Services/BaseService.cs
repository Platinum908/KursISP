using KursProjectISP31.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursProjectISP31.Services
{
    public abstract class BaseService<T>
    {
        private SqlConnection objSqlconnection;
        private SqlCommand objSqlCommand;
        public BaseService()
        {
            objSqlconnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);
            objSqlCommand = new SqlCommand();
            objSqlCommand.Connection = objSqlconnection;
            objSqlCommand.CommandType = CommandType.StoredProcedure;
        }
        public abstract List<T> GetAll();
        public abstract bool Add(T obj);
        public abstract bool Update(T obj);
        public abstract bool Delete(int id);
    }
}
