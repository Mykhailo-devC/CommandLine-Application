using CommandLine_App.Abstraction;
using CommandLine_App.GlobalCommands.ShowCommandChildren;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.Pools
{
    public class ShowPool : PoolAbstraction<Command>
    {
        public Dictionary<string, Command> Pool { get; set; }
        public ShowPool()
        {
            Pool = new Dictionary<string, Command>()
            {
                { "all", new ShowAllCommand() },
                { "memory", new ShowMemoryCommand() },
                { "name", new ShowNameCommand() },
                { "pid", new ShowPidCommand() },
            };
        }
    }
}
