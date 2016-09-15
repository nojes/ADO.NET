using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Find
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] cars = { "Alfa Romeo", "BMW", "Aston Martin", "Audi", "Mersedes Benz", "Chevrolet", "Subaru" };


            var query = cars.Where(car => car.ToLower().StartsWith("a"));
            foreach (var item in query) {
                Console.WriteLine(item);
            }

            Console.WriteLine();
            var query2 = from c in cars
                         where c.ToLower().StartsWith("a")
                         select c;
            foreach (var item in query2) {
                Console.WriteLine(item);
            }

        }
    }
}
