using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CommandLine_App.Abstraction
{
    public abstract class Command
    {
        public string Name { get; set; }
        public string ArgumentDescription { get; set; }
        public abstract bool Execute(params string[] param);
        public abstract override string ToString();

        public void PrintArgumentTip()
        {
            Console.Write(ArgumentDescription);
        }
    }
}
