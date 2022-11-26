using CommandLine_App.Abstraction;
using CommandLine_App.GlobalCommands.KillCommandChildren;
using CommandLine_App.GlobalCommands.StartCommandChildren;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.Pools
{
    public class StartPool : IPool<Command>
    {
        public Dictionary<string, Command> Pool { get; set; }
        public StartPool()
        {
            Pool = new Dictionary<string, Command>()
            {
                { "name", new StartNameCommand() },
            };
        }
    }
}
