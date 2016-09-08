using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_DataAdapter
{
    class Program
    {
        static void Main(string[] args)
        {
            string conString = ConfigurationManager.ConnectionStrings["shop"].ConnectionString;
            SqlConnection connection = new SqlConnection(conString);

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM dbo.Category" , connection);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            adapter.DeleteCommand = builder.GetDeleteCommand();
            adapter.InsertCommand = builder.GetInsertCommand();
            adapter.UpdateCommand = builder.GetUpdateCommand(true);

            try
            {
                Console.WriteLine(" Delete: \n{0}", adapter.DeleteCommand.CommandText);
                Console.WriteLine(" Insert: \n{0}", adapter.InsertCommand.CommandText);
                Console.WriteLine(" Update: \n{0}", adapter.UpdateCommand.CommandText);

                DataTable category = new DataTable("Category");
                adapter.Fill(category);

                PrintLine();

                foreach (DataColumn c in category.Columns)
                {
                    Console.Write(" {0}\t", c.ColumnName);
                }

                Console.WriteLine();
                PrintLine();

                foreach (DataRow row in category.Rows)
                {
                    Console.WriteLine(" {0}\t\t {1}", row[0], row[1]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]: {0}", ex.Message);
            }
            Console.WriteLine();
        }

        static void PrintLine()
        {
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("-");
            }
        }
    }
}
