using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_ConnectionString
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @"
                Data Source=localhost;
                Initial Catalog = ShopAdo;
                Integrated Security=False; 
                user=sa;
                password=student;
                Connect Timeout=15;";
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
                if(connection.State == System.Data.ConnectionState.Open) {
                    connection.Close();
                }
            }
        }
    }
}
