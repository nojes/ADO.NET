using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_SqlConnectionStringBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder();
            sb.DataSource = "localhost";
            sb.InitialCatalog = "ShopAdo";
            sb.IntegratedSecurity = false;
            sb.UserID = "sa";
            sb.Password = "student";

            SqlConnection connection = new SqlConnection(sb.ToString());
            try
            {
                connection.Open();
                Console.WriteLine("Connection success!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]: {0}", ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open) {
                    connection.Close();
                }
            }
        }
    }
}
