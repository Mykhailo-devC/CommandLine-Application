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
        public ShowPool()
        {
            Pool = new Dictionary<string, Command>()
            {
                { "all", new ShowAll() },
                { "memory", new ShowMemory() },
                { "name", new ShowName() },
                { "pid", new ShowPid() },
            };
        }
    }
}
