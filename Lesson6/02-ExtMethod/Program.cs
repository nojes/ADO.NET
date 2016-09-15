using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_ExtMethod
{
    static class StringExt
    {
        public static string Method1(this string s, double d)
        {
            return string.Format(" {0} - {1}", s, d);
        }

        public static void Method2(this string s, params int[] arr)
        {
            Console.WriteLine(s);
            foreach (var item in arr)
            {
                Console.WriteLine(item + " ");
            }
        }

        public static string GetInt(this int a)
        {
            return string.Format("this int = {0}", a);
        }
    }

    public static class Console
    {
        public static void WriteLine(string value)
        {
            System.Console.WriteLine(value);
        }

        public static void WriteBlueLine(string value)
        {
            System.ConsoleColor currentColor = System.Console.ForegroundColor;

            System.Console.ForegroundColor = System.ConsoleColor.Blue;
            System.Console.WriteLine(value);

            System.Console.ForegroundColor = currentColor;
        }

        public static void WriteError(string value)
        {
            System.ConsoleColor currentColor = System.Console.ForegroundColor;

            System.Console.ForegroundColor = System.ConsoleColor.Red;
            System.Console.WriteLine("[ERROR]: " + value);

            System.Console.ForegroundColor = currentColor;
        }

        public static System.ConsoleKeyInfo ReadKey(bool intercept)
        {
            return System.Console.ReadKey(intercept);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string s = "String";
            string s2 = s.Method1(3.14);
            Console.WriteLine(s2);
            Console.WriteBlueLine("WriteBlueLine");
            Console.WriteError("something wrong");
        }
    }
}
