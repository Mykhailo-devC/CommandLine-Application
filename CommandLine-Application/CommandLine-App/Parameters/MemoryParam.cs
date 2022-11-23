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
        public override string ArgumentDescription { get; set; }
        public MemoryParam()
        {
            Name = "memory";
            NamePool = new List<string>()
            {
                Name,
                "-m"
            };
            ArgumentDescription = "This parameter a memory";
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
