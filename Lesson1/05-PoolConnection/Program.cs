using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_PoolConnection
{
    class Program
    {
        static void Main(string[] args)
        {
            string conString = ConfigurationManager.ConnectionStrings["Shop"].ConnectionString;
            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(conString);
            sb.Pooling = true;
            sb.MaxPoolSize = 5;

            // With pooling
            SqlConnection connection = new SqlConnection(sb.ToString());
            Console.WriteLine("1000 connect, MaxPoolSize = 5");
            DateTime dStart = DateTime.Now;
            for (int i = 0; i < 1000; i++)
            {
                connection.Open();
                connection.Close();
            }
            TimeSpan span = DateTime.Now - dStart;
            Console.WriteLine("Time: {0} \n\n", span.Milliseconds);

            // Without pooling
            sb.Pooling = false;
            connection = new SqlConnection(sb.ToString());
            Console.WriteLine("1000 connect, MaxPoolSize = 5, Pooling = false");
            dStart = DateTime.Now;
            for (int i = 0; i < 1000; i++)
            {
                connection.Open();
                connection.Close();
            }
            span = DateTime.Now - dStart;
            Console.WriteLine("Time: {0} \n\n", span.Milliseconds);
        }
    }
}
