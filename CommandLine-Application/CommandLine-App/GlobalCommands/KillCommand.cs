using CommandLine_App.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.Commands
{
    public abstract class KillCommand : Command
    {
        public override string Name { get; set; }
        public override abstract string ArgumentDescription { get; set; }
        public KillCommand()
        {
            Name = "kill";
        }

        public override abstract bool Execute(params string[] param);

        public override string ToString()
        {
            return $"\nCommand '{Name}' - stop specifeied processes.";
        }

        public override abstract void PrintBaseToString();

    }
}
