using CommandLine_App.Abstraction;
using CommandLine_App.GlobalCommands.ShowCommandChildren;
using CommandLine_App.ProcessService;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.Pools
{
    public class ShowPool : IPool<Command>
    {
        public Dictionary<string, Command> Pool { get; set; }
        public ShowPool(ProcessWrapper wrapper)
        {
            Pool = new Dictionary<string, Command>()
            {
                { "all", new ShowAllCommand(wrapper) },
                { "memory", new ShowMemoryCommand(wrapper) },
                { "name", new ShowNameCommand(wrapper) },
                { "pid", new ShowPidCommand(wrapper) },
            };
        }
    }
}
