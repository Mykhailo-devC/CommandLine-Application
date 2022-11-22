using CommandLine_App.Abstraction;
using CommandLine_App.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App
{
    public static class CommandPool
    {
        public static readonly Dictionary<string, Command> Pool = new Dictionary<string, Command>()
            {
                {"show", new ShowCommand() },
                {"kill", new KillCommand() },
                { "refresh", new RefreshCommand() },
                { "start", new StartCommand() }
            };
    }
}
