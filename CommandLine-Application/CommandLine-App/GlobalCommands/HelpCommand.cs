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
        public override string Name { get; set; }
        public override abstract string ArgumentDescription { get; set; }
        protected CommandPool _pool;

        public HelpCommand(CommandPool pool)
        {
            Name = "help";
            _pool = pool;
        }

        public override abstract bool Execute(params string[] param);

        public override void PrintBaseToString()
        {
            Console.WriteLine(this.ToString());
        }

        public override string ToString()
        {
            return $"\n{Name} - shows all commands and their avalable parameters.";
        }
    }
}
