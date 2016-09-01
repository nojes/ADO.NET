using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_ExucuteScalar
{
    class Program
    {
        static void Main(string[] args)
        {
            string conString = ConfigurationManager.ConnectionStrings["Shop"].ConnectionString;
            SqlConnection connect = new SqlConnection(conString);

            SqlCommand cmdCreateTestTable = new SqlCommand(@"
                CREATE TABLE test (
                    id INT IDENTITY PRIMARY KEY, 
                    name NVARCHAR(10)
                )", 
                connect
            );

            SqlCommand cmdCount = new SqlCommand(@"
                SELECT count(*) AS count FROM dbo.Manufacturer",
                connect
            );

            try
            {
                connect.Open();

                //cmdCreateTestTable.ExecuteNonQuery();
                object count = cmdCount.ExecuteScalar();

                Console.WriteLine("Manufacturer count: {0}", count);
                Console.WriteLine("Commands successfuly execute!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]: {0}", ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }
    }
}
