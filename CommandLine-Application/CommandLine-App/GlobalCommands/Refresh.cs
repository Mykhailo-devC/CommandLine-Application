using CommandLine_App.Abstraction;
using CommandLine_App.Pools;
using CommandLine_App.ProcessService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CommandLine_App.Commands
{
    public abstract class Refresh : Command
    {
        public override abstract bool Execute(params string[] param);

        public override string ToString()
        {
            return $"\nCommand '{this.GetType().Name}' - refresh specifeied processes.";
        }
    }
}
