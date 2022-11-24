using CommandLine_App.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.Parameters
{
    public class MemoryParam : Parameter
    {
        public override string Name { get; set; }
        public override List<string> NamePool { get; set; }
        public override short ArgumentsCount { get; set; }
        public override string ArgumentDescription { get; set; }
        public MemoryParam()
        {
            Name = "memory";
            NamePool = new List<string>()
            {
                Name,
                "-m"
            };
            ArgumentsCount = 2;
            ArgumentDescription = "Show memory (int value), " +
                "shows the process using [value] memory, \nlike [show memory 200]." +
                "\n\nShow memory (int start, int end) shows all processes using " +
                "memory between [start] and [end] values, \nlike [show memory 500 1000].\n";
        }

        

        public override string ToString()
        {
            var str = new StringBuilder("processes with define memory.");
            if (NamePool.Count > 1)
            {
                PrintShortCuts(str);
            }

            return str.ToString();
        }
    }
}
