using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _02_Yield
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<int> enum1 = GetSequenceYield();

            do
            {
                foreach (var item in enum1)
                {
                    Console.Write(item + "\t");
                    Thread.Sleep(32);
                }
                Console.ReadKey();
            } while (true);
            
        }

        static IEnumerable<int> GetSequenceYield()
        {
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                yield return rand.Next(0, 9);
            }
        }
    }
}
