using CommandLine_App.Abstraction;
using CommandLine_App.Commands;
using CommandLine_App.GlobalCommands.ShowCommandChildren;
using CommandLine_App.HelperService;
using CommandLine_App.Pools;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CommandLine_App.GlobalCommands.HelpCommandChildren
{
    public class HelpGlobalCommand : HelpCommand
    {
        public new string Name { get; set; }
        public override string ArgumentDescription { get; set; }

        public HelpGlobalCommand(IPool<Dictionary<string, Command>> pool) : base(pool)
        {
            Name = "help";
            ArgumentDescription = "This parameter takes no arguments, like [help]\n";
        }

        public override bool Execute(params string[] param)
        {
            try
            {
                if (param.Any())
                {
                    Log.Warning("[{1}] User inputs incorrect count of parameters, [params = '{0}']", param, this.GetType());
                    PrintArgumentTip();
                    return false;
                }

                return HelpGlobal();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[{1}] Exeption has been thrown from Execute! [params = '{0}']",param, this.GetType());
                return false;
            }
        }

        public override void PrintBaseToString()
        {
            Console.WriteLine(base.ToString());
        }

        public override string ToString()
        {
            return $"\t'{Name}' - shows all commands and their avalable parameters.";
        }

        private bool HelpGlobal()
        {
            try
            {
                foreach(var com in _pool.Pool.Values)
                {
                    com.Values.First().PrintBaseToString();

                    foreach(var par in com.Values)
                    {
                        Console.WriteLine(par.ToString());
                    }
                }

                Log.Information("[{0}] Execute has been finished successfully!", this.GetType());
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

