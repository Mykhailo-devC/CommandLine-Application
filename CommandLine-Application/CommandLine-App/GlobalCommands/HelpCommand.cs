using CommandLine_App.Abstraction;
using CommandLine_App.HelperService;
using CommandLine_App.Pools;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.Commands
{
    public abstract class HelpCommand : Command
    {
        protected readonly IPool<Dictionary<string, Command>> _pool;

        public HelpCommand(IPool<Dictionary<string, Command>> pool)
        {
            Name = CommandType.help.ToString();
            _pool = pool;
        }

        public override abstract bool Execute(params string[] param);

        public override string ToString()
        {
            return $"\nCommand '{Name}' - shows information about existing commandshe";
        }
    }
}
