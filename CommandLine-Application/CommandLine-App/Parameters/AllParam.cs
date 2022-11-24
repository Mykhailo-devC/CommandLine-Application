using CommandLine_App.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.Parametrs
{
    public class AllParam : Parameter
    {
        public override string Name { get; set; }
        //For future allias
        public override short ArgumentsCount { get; set; }
        public override List<string> NamePool { get; set; }
        public override string ArgumentDescription { get; set; }

        public AllParam()
        {
            Name = "all";
            NamePool = new List<string>()
            {
                Name,
                "-a",
            };
            ArgumentsCount = 0;
            ArgumentDescription = "This parameter takes no arguments.\n";
        }

        public override string ToString()
        {
            var str = new StringBuilder("all processes.");
            if(NamePool.Count > 1)
            {
                PrintShortCuts(str);
            }
                
            return str.ToString();
        }
    }
}
