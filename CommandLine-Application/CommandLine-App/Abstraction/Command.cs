using CommandLine_App.Utilities.Implementations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CommandLine_App.Abstraction
{
    public abstract class Command
    {
        protected Command()
        {
            _processWrapper = new ProcessWrapper();
        }
        public string ArgumentDescription { get; set; }
        protected ProcessWrapper _processWrapper { get; set; }
        public abstract bool Execute(params string[] param);
        public abstract override string ToString();

        public void PrintArgumentTip()
        {
            Console.Write(ArgumentDescription);
        }
    }
}
