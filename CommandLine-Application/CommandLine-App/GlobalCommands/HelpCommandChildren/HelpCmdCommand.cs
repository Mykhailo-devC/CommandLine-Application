using CommandLine_App.Abstraction;
using CommandLine_App.Commands;
using CommandLine_App.HelperService;
using CommandLine_App.Pools;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CommandLine_App.GlobalCommands.HelpCommandChildren
{
    public class HelpCmdCommand : HelpCommand
    {
        public HelpCmdCommand(IPool<Dictionary<string, Command>> pool) : base(pool)
        {
            Name += "[command]";
            ArgumentDescription = "Help (string value), like [help show].";
        }

        public override bool Execute(params string[] param)
        {
            try
            {
                if (param.Length! != 1)
                {
                    Log.Warning("[{1}] User inputs incorrect count of parameters, [params = '{0}']", param, this.GetType());
                    PrintArgumentTip();
                    return false;
                }

                return HelpCmd(param.First());
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}][parameters = {param}]");
                return false;
            }
        }

        public override string ToString()
        {
            return $"\t'{Name}' - shows all parameters that spesified command can take.";
        }

        private bool HelpCmd(string arg)
        {
            if (!_pool.Pool.ContainsKey(arg))
            {
                Log.Warning("[{1}] User inputs incorrect parameters, [params = '{0}']", arg, this.GetType());
                PrintArgumentTip();
                return false;
            }


            foreach (var par in _pool.Pool[arg].Values)
            {
                Console.WriteLine(par.ToString());
            }

            Log.Information("[{0}] Execute has been finished successfully!", this.GetType());
            return true;
        }
    }
}
