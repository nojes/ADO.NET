using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace _01_SqlCommand
{
    class Program
    {
        static void Main(string[] args)
        {
            string conString = ConfigurationManager.ConnectionStrings["Shop"].ConnectionString;
            SqlConnection connect = new SqlConnection(conString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connect;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "dbo.GetManufacturer";

            using (connect)
            {
                connect.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows) {
                    Console.WriteLine("\n ID \t NAME");
                    Console.WriteLine(" ----------------");
                    while (dr.Read()) {
                        Console.WriteLine(" {0} \t {1}", dr[0], dr[1]);
                    }
                    Console.WriteLine();
                }
                else {
                    Console.WriteLine(" Empty rows.");
                }
            }

        }
    }
}
