using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace ConsoleAppForSportsBook.DB
{
    class DBUtils
    {
        public static SqlConnection GetDBConnection()
        {
            string datasource = @".\SQLEXPRESS";
            string database = "SportsBook";

           
            return DbSQLServerUtils.GetDBConnection(datasource, database);
        }
    }
}
