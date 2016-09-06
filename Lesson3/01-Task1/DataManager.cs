using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Task1
{
    /// <summary>
    /// TODO
    /// </summary>
    class DataManager
    {
        SqlConnection connection;

        public DataManager(SqlConnection connection)
        {
            this.connection = connection;
        }

        public SqlConnection Connection
        {
            set { connection = value; }
        }

        public void addCity(City c)
        {

        }
    }
}
