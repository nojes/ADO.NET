using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_DataTable
{
    class Program
    {
        static void Main(string[] args)
        {
            DataTable table = InitDataTable();

            DataRow row = table.NewRow();
            row["firstname"] = "Ivan";
            row["lastname"] = "Tarasenko";
            row["date"] = new DateTime(1999, 12, 31);

            DataRow row2 = table.NewRow();
            row2["firstname"] = "Vasya";
            row2["lastname"] = "Pupkin";
            row2["date"] = new DateTime(1970, 01, 02);

            table.Rows.Add(row);
            table.Rows.Add(row2);

            foreach (DataRow r in table.Rows)
            {
                Console.WriteLine(" {0} {1} {2} {3}",
                    r["id"], r["firstname"], r["lastname"], r["date"]
                );
            }
        }

        static DataTable InitDataTable()
        {
            DataTable table = new DataTable("Person");
            DataColumn id = new DataColumn("id", typeof(int));
            id.ReadOnly = true;
            id.AllowDBNull = false;
            id.Unique = true;
            id.AutoIncrement = true;
            id.AutoIncrementStep = 1;
            id.AutoIncrementSeed = 1;

            table.Columns.AddRange(new DataColumn[] {
                id,
                new DataColumn("firstname", typeof(string)),
                new DataColumn("lastname", typeof(string)),
                new DataColumn("date", typeof(DateTime))
            });

            return table;
        }
    }
}
