using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_CommandOutputParam
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
            cmd.CommandText = "dbo.InsertCategory";

            cmd.Parameters.AddWithValue("@CategoryName", "Test").Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@CategoryId", 0).Direction = ParameterDirection.Output;

            try {
                connect.Open();
                int count = cmd.ExecuteNonQuery();

                Console.WriteLine(" Inserted rows: {0}", count);
                Console.WriteLine(" CategoryId identity: {0}", cmd.Parameters["@CategoryId"].Value);
            }
            catch (Exception ex) {
                Console.WriteLine("[ERROR]: {0}", ex.Message);
            }
            finally {
                connect.Close();
            }
        }
    }
}
