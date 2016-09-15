using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_linqToArray
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr1 = new int[] { 1, 4, 5, 79, 24 };
            int[] arr2 = new int[] { 5, 3, 56, -13, 74 };

            var query = arr1.Where(p => p > arr2.Average()).ToArray();

            foreach (var item in query) {
                Console.WriteLine(item + " ");
            }
        }
    }
}
