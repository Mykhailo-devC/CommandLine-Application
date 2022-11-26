using CommandLine_App.Abstraction;
using CommandLine_App.GlobalCommands.HelpCommandChildren;
using CommandLine_App.HelperService;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.Pools
{
    public class HelpPool : IPool<Command>
    {
        public Dictionary<string, Command> Pool { get; set; }

        public HelpPool(CommandPool pool)
        {
            Pool = new Dictionary<string, Command>()
            {
                { "help", new HelpGlobalCommand(pool) },
                { "help [command]", new HelpCmdCommand(pool) },
                { "help [command] [parameter]", new HelpParamCommand(pool, new Helper(pool)) }
            };
        }
    }
}
