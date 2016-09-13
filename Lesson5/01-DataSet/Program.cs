using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_DataSet
{
    class Program
    {
        static void Main(string[] args)
        {
            string conString = ConfigurationManager.ConnectionStrings["Region"].ConnectionString;
            SqlConnection connect = new SqlConnection(conString);

            DataSet set = new DataSet();
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Region; SELECT * FROM City;", connect))
            {
                adapter.Fill(set);
            }

            int count = set.Tables.Count;
            Console.WriteLine(" Count of tables: {0}", count);

            Console.WriteLine(" Tables: ");
            foreach (DataTable t in set.Tables) {
                Console.WriteLine("\t{0}", t.TableName);
            }

            DataTable Region = set.Tables[0];
            //foreach (DataRow r in Region.Rows) {
            //    Console.WriteLine(" {0} {1}", r["RegionId"], r["RegionName"]);
            //}

            int realationsCount = set.Relations.Count;
            Console.WriteLine(" realationsCount: {0}", realationsCount);
        }
    }
}
