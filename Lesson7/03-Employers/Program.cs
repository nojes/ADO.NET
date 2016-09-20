using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace _03_Employers
{
    class Program
    {
        const string DATA_ROOT = "../../Data/";
        static string fileName = "employers.dat";

        static void Main(string[] args)
        {
            List<Employee> employers = DataManager.GetEmployee();
            var query1 = employers
                .Where(e => DateTime.Now.Year - e.DateBirthday.Year <= 35)
                .Select(e => e); // not necessary
            var query2 = employers
                .Where(e => (DateTime.Now.Year - e.DateBirthday.Year <= 35)
                        && (DateTime.Now.Year - e.DateBirthday.Year >= 25));
            var query3 = employers
                .Where(e => e.FirstName.ToLower().StartsWith("i"));
            var query4 = employers
                .Where(e => e.FirstName.ToLower().StartsWith("d")
                        || e.LastName.ToLower().StartsWith("d"));
            var query5 = employers
                .Where(e => e.FirstName.ToLower().Count(c => c == 'a') == 2);

            var select1 = (from e in employers
                          select new EmployeeForManager()
                          {
                              EmployeeId = e.EmployeeId,
                              FirstName = e.FirstName,
                              LastName = e.LastName,
                              Age = DateTime.Now.Year - e.DateBirthday.Year
                          }).ToList();

            writeItems("Years <= 35", query1);
            writeItems("Years <= 35 and >= 25", query2);
            writeItems("FirstName start with 'i'", query3);
            writeItems("FirstName or LastName start with 'd'", query4);
            writeItems("FirstName contain 2 'a'", query5);

            writeItems("SELECT EmployeeId, FirstName, LastName, Age \nFROM Employers", select1);


            FileStream fs = new FileStream(DATA_ROOT + fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, select1);
            fs.Close();
        }

        static void writeItems<T>(string title, IEnumerable<T> items)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("{0}:", title);
            Console.ForegroundColor = currentColor;

            foreach (var item in items) {
                Console.WriteLine("\t{0}", item);
            }
            Console.WriteLine();
        }
    }
}
