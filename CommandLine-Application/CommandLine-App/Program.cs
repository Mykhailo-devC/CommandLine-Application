using CommandLine_App.Abstraction;
using CommandLine_App.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandLine_App
{
    public class Program
    {
        public static void Main(string[] args)
        { 
            foreach(var c in CommandPool.Pool)
            {
                Console.WriteLine(c.Value.ToString());
            }
            
        }
    }
}
