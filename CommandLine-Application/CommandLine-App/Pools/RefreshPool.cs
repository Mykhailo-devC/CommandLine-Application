using CommandLine_App.Abstraction;
using CommandLine_App.GlobalCommands.RefreshCommandChildren;
using CommandLine_App.GlobalCommands.ShowCommandChildren;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.Pools
{
    public class RefreshPool : IPool<Command>
    {
        public Dictionary<string, Command> Pool { get; set; }
        public RefreshPool()
        {
            Pool = new Dictionary<string, Command>()
            {
                { "name", new RefreshNameCommand() },
                { "pid", new RefreshPidCommand() },
            };
        }
    }
}
