using CommandLine_App.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.Parameters
{
    public class PidParam : Parameter
    {
        public override string Name { get; set; }
        public override List<string> NamePool {get; set;}
        public PidParam()
        {
            Name = "pid";
            NamePool = new List<string>()
            {
                Name,
                "-p"
            };
        }
        public override string ToString()
        {
            var str = new StringBuilder("processes with define process identifier.");
            if (NamePool.Count > 1)
            {
                PrintShortCuts(str);
            }

            return str.ToString();
        }
    }
}
