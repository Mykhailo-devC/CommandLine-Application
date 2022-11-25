using CommandLine_App.Abstraction;
using CommandLine_App.Commands;
using CommandLine_App.GlobalCommands.HelpCommandChildren;
using CommandLine_App.GlobalCommands.ShowCommandChildren;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CommandLine_App.Pools
{
    public class CommandPool : PoolAbstraction<Dictionary<string, Command>>
    {
        public Dictionary<string, Dictionary<string, Command>> Pool { get; set; }
        public CommandPool()
        {
            Pool = new Dictionary<string, Dictionary<string, Command>>()
            {
                { "show", new ShowPool().Pool },
                /*{ "kill", new KillCommand() },
                { "refresh", new RefreshCommand() },
                { "start", new StartCommand() }*/
                { "help", new HelpPool(this).Pool }
            };
        }
    }
}
