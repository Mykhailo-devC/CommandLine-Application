using CommandLine_App.Abstraction;
using CommandLine_App.Commands;
using CommandLine_App.GlobalCommands.ShowCommandChildren;
using CommandLine_App.HelperService;
using CommandLine_App.Pools;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommandLine_App.GlobalCommands.HelpCommandChildren
{
    public class HelpGlobalCommand : HelpCommand
    {
        public HelpGlobalCommand(IPool<Dictionary<string, Command>> pool) : base(pool)
        {
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
                Log.Error(ex, $"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}][parameters = {param}]");
                return false;
            }
        }

        public override string ToString()
        {
            return $"\t'{Name}' - shows all commands and their avalable parameters.";
        }

        private bool HelpGlobal()
        {
            foreach (var com in _pool.Pool.Values)
            {
                foreach (var par in com.Values)
                {
                    Console.WriteLine(par.ToString());
                }
            }

            Log.Information("[{0}] Execute has been finished successfully!", this.GetType());
            return true;
        }
    }
}

