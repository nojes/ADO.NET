using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace _01_Join
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employers = DataManager.GetEmployee();
            List<EmpPromotion> promotions = DataManager.GetEmpPromotion();
            List<JobTitle> jobs = DataManager.GetJobTitle();

            var query = from e in employers
                    join p in promotions on e.EmployeeId equals p.EmployeeId
                    where e.EmployeeId == 1
                    select new { e, p };

            foreach (var item in query) {
                Console.WriteLine("{0}\t{1:d}\t{2}",
                    item.e.ToString(), item.p.HireDate, item.p.Salary
                );
            }

            var queryL = employers.Where(e => e.EmployeeId == 1).Join(promotions, em => em.EmployeeId, pr => pr.EmployeeId, (e, p) => new { e, p });

            Console.WriteLine("--------");
            foreach (var item in queryL) {
                Console.WriteLine("{0}\t{1:d}\t{2}",
                    item.e.ToString(), item.p.HireDate, item.p.Salary
                );
            }

            var query2 = from em in employers
                         join pr in promotions on em.EmployeeId equals pr.EmployeeId into Group
                         from gr in Group.DefaultIfEmpty()
                         select new { em, gr };

            Console.WriteLine("--------");
            foreach (var item in query2) {
                Console.WriteLine("{0}\t{1:d}\t{2}",
                    item.em.ToString(),
                    (item.gr == null) ? "NULL" : item.gr.HireDate.ToString(),
                    (item.gr == null) ? "NULL" : item.gr.Salary.ToString()
                );
            }
        }
    }
}
