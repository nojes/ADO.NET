using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_ConfigFileWithoutLoginAndPass
{
    class Program
    {
        static void Main(string[] args)
        {
            string login = string.Empty, password = string.Empty;

            label1:
            Console.WriteLine();
            Console.WriteLine("Input login:");
            login = Console.ReadLine();

            Console.WriteLine("Input password:");
            password = Console.ReadLine();

            string conString = ConfigurationManager.ConnectionStrings["Shop"].ConnectionString;
            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(conString);
            sb.Add("User", login);
            sb.Add("Password", password);

            SqlConnection connection = new SqlConnection(sb.ToString());
            try
            {
                connection.Open();
                Console.WriteLine("Connection success!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]: {0}", ex.Message);
                goto label1;
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }

        }
    }
}
