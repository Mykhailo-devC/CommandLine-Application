using CommandLine_App.Abstraction;
using CommandLine_App.GlobalCommands.KillCommandChildren;
using CommandLine_App.GlobalCommands.RefreshCommandChildren;
using CommandLine_App.ProcessService;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.Pools
{
    public class KillPool : IPool<Command>
    {
        public Dictionary<string, Command> Pool { get; set; }
        public KillPool(ProcessWrapper wrapper)
        {
            Pool = new Dictionary<string, Command>()
            {
                { "name", new KillNameCommand(wrapper) },
                { "pid", new KillPidCommand(wrapper) },
                { "memory", new KillMemoryCommand(wrapper) }
            };
        }
    }
}
