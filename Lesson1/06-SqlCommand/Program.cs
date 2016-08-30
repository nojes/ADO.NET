using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_SqlCommand
{
    class Program
    {
        static void Main(string[] args)
        {
            string conString = ConfigurationManager.ConnectionStrings["Shop"].ConnectionString;
            SqlConnection connect = new SqlConnection(conString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connect;
            cmd.CommandText = "SELECT CategoryId, CategoryName FROM dbo.Category";

            try
            {
                connect.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(
                            " {0}\t{1}", 
                            reader.GetInt32(0), reader.GetString(1)
                        );
                    }
                }
                else
                {
                    Console.WriteLine("Nothing not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]: {0}", ex.Message);
            }
            finally
            {
                if (connect.State != System.Data.ConnectionState.Closed)
                    connect.Close();
            }
        }
    }
}
