using System;
using System.Collections;

namespace IEnumerable_example
{
    class Program
    {
        static void Main(string[] args)
        {

            int[] array = new int[] { 1, 22, 58, 584, 1 };

            Console.WriteLine("foreach");
            foreach (var item in array)
            {
                Console.Write("{0} ", item);
            }

            Console.WriteLine("\nIEnumerator");
            IEnumerable enumerable = array;
            IEnumerator enumerator = enumerable.GetEnumerator();
            enumerator.Reset();
            while (enumerator.MoveNext())
            {
                Console.Write("{0} ", enumerator.Current);
            }
            Console.WriteLine();
        }
    }
}
