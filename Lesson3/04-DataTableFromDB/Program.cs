using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_DataTableFromDB
{
    class Program
    {
        static void Main(string[] args)
        {
            string conString = ConfigurationManager.ConnectionStrings["Shop"].ConnectionString;
            SqlConnection connect = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Manufacturer");
            cmd.Connection = connect;

            DataTable Manufacturer = new DataTable("Manufacturer");

            try
            {
                connect.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                for (int i = 0; i < reader.FieldCount; i++) {
                    Manufacturer.Columns.Add(new DataColumn(reader.GetName(i), reader.GetFieldType(i)));
                }

                while (reader.Read())
                {
                    DataRow row = Manufacturer.NewRow();

                    for (int i = 0; i < reader.FieldCount; i++) {
                        row[i] = reader[i];
                    }

                    Manufacturer.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]: {0}", ex.Message);
            }
            finally
            {
                connect.Close();
            }


            foreach (DataRow row in Manufacturer.Rows) {
                Console.WriteLine(" {0} {1}", row[0], row[1]);
            }
        }
    }
}
