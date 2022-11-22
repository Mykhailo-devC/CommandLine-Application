using CommandLine_App.Abstraction;
using CommandLine_App.Commands;
using System;
using System.Collections.Generic;

namespace CommandPool
{
    public class Commands
    {
        public readonly Dictionary<string, Command> cmdPool = new Dictionary<string, Command>()
            {
                {"show", new ShowCommand() },
                {"kill", new KillCommand() },
                {"refresh", new RefreshCommand() },
                {"start", new StartCommand() }
            };
    }
}
