using CommandLine_App.Abstraction;
using CommandLine_App.Pools;
using CommandLine_App.ProcessService;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.Commands
{
    public abstract class KillCommand : Command
    {
        protected ProcessWrapper _wrapper;
        public KillCommand(ProcessWrapper wrapper)
        {
            _wrapper = wrapper;
            Name = CommandType.kill.ToString();
        }

        public override abstract bool Execute(params string[] param);

        public override string ToString()
        {
            return $"\nCommand '{Name}' - stop specifeied processes.";
        }

    }
}
