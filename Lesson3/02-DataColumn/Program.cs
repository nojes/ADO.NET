using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_DataColumn
{
    class Program
    {
        static void Main(string[] args)
        {
            DataTable dTable = new DataTable("Test");
            dTable.Columns.Add("id", typeof(int));
            dTable.Columns.Add("name", typeof(string));
            DataColumn dColumn = new DataColumn("date", typeof(DateTime));
            dTable.Columns.Add(dColumn);

            foreach (DataColumn i in dTable.Columns) {
                Console.WriteLine(" {0} => {1}", i.ColumnName, i.DataType);
            }

            PrintLine();

            DataRow dRow = dTable.NewRow();

            dRow[0] = 1;
            dRow[1] = "row";
            dRow["date"] = DateTime.Now;

            dTable.Rows.Add(dRow);

            foreach (DataRow r in dTable.Rows)
            {
                Console.WriteLine(" {0} {1} {2}", 
                    r["id"], r["name"], r["date"]
                );
            }

        }

        static void PrintLine()
        {
            for (int i = 0; i < Console.WindowWidth; i++) {
                Console.Write("-");
            }
        }
    }
}
