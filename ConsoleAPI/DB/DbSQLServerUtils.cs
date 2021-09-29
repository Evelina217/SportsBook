using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConsoleAppForSportsBook.DB
{
    class DbSQLServerUtils
    {
        public static SqlConnection GetDBConnection(string datasource, string database)
        {
            // Data Source==(localdb)\MSSQLLocalDB;Initial Catalog=SportsBook;Integrated Security = True"

            
            string connString = @"Data Source=" + datasource + ";Initial Catalog="
                         + database + ";Trusted_Connection=True";
;
            
            SqlConnection conn = new SqlConnection(connString);
            return conn;
        }
    }
}
