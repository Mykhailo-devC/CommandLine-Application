using CommandLine_App.Abstraction;
using CommandLine_App.Pools;
using CommandLine_App.ProcessService;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.Commands
{
    public abstract class StartCommand : Command
    {
        protected ProcessWrapper _wrapper;
        public StartCommand(ProcessWrapper wrapper)
        {
            _wrapper = wrapper;
            Name = CommandType.start.ToString();
        }

        public override abstract bool Execute(params string[] param);

        public override string ToString()
        {
            return $"\nCommand '{Name}' - starts specifeied processes.";
        }

    }
}
