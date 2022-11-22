using CommandLine_App.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.Parameters
{
    public class NameParam : Parameter
    {
        public override string Name { get; set; }
        public override List<string> NamePool { get; set; }
        public NameParam()
        {
            Name = "name";
            NamePool = new List<string>()
            {
                Name,
                "-n"
            };
        }

        public override string ToString()
        {
            var str = new StringBuilder("processes with define name.");
            if (NamePool.Count > 1)
            {
                PrintShortCuts(str);
            }

            return str.ToString();
        }
    }
}
