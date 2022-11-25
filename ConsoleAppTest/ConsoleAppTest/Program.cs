using System;
using System.Runtime.CompilerServices;

namespace ConsoleAppTest
{
    public class Program
    {
        public static void meth1(params string[] param)
        {
            foreach (string s in param)
            {

            }
            testMeth1(param);
        }

        public static void testMeth1(string arg)
        {
            Console.WriteLine("Method with one parameter : {0}", arg);
        }
        public static void testMeth1(params string[] args)
        {
            Console.WriteLine("Method with two parameter : {0}, {1}", args[0], args[1]);
        }
        static void Main(string[] args)
        {
            meth1("1");
        }
    }
}
