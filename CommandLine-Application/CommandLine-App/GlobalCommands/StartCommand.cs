using CommandLine_App.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.Commands
{
    public abstract class StartCommand : Command
    {
        public override string Name { get; set; }
        public override abstract string ArgumentDescription { get; set; }
        public StartCommand()
        {
            Name = "start";
        }

        public override abstract bool Execute(params string[] param);

        public override string ToString()
        {
            return $"\nCommand '{Name}' - starts specifeied processes.";
        }

        public override abstract void PrintBaseToString();

    }
}
