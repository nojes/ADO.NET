using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_homework
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] cars = { "Alfa Romeo", "BMW", "Aston Martin", "Audi", "Mersedes Benz", "Chevrolet", "Subaru", "Ford"};

            var q1 = cars.Where(car => { return car.Length == 4; });
            var q1_2 = from car in cars
                       where car.Length == 4
                       select car;
            var q2 = cars.Where(car => car.Count(x => x == 'o') == 2);
            var q3 = cars.Where(car => car.Length < 5).Select(car => new { car, car.Length });

            writeItems("Cars, where mark length = 4", q1);
            writeItems("Cars, where mark length = 4 (2)", q1_2);
            writeItems("Cars, where mark contain 2 'o'", q2);
            writeItems("Anonym objects", q3);
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
        }
    }
}
