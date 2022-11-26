using CommandLine_App.Abstraction;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CommandLine_App.Commands
{
    public abstract class RefreshCommand : Command
    {
        public override string Name { get; set; }
        public override abstract string ArgumentDescription { get; set; }
        public RefreshCommand()
        {
            Name = "refresh";
        }

        public override abstract bool Execute(params string[] param);

        public override string ToString()
        {
            return $"\nCommand '{Name}' - refresh specifeied processes.";
        }

        public override abstract void PrintBaseToString();

    }
}
