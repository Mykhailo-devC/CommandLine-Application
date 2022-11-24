using CommandLine_App.Abstraction;
using CommandLine_App.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App
{
    public class CommandPool
    {
        public static Dictionary<string, Command> Pool = new Dictionary<string, Command>()
            {
                { "show", new ShowCommand() },
                { "kill", new KillCommand() },
                { "refresh", new RefreshCommand() },
                { "start", new StartCommand() },
                { "help", new HelpCommand() }
            };
    }
}
