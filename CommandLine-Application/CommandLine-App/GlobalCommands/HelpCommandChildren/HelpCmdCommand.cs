using CommandLine_App.Commands;
using CommandLine_App.HelperService;
using CommandLine_App.Pools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandLine_App.GlobalCommands.HelpCommandChildren
{
    public class HelpCmdCommand : HelpCommand
    {
        public new string Name { get; set; }
        public override string ArgumentDescription { get; set; }

        public HelpCmdCommand(CommandPool pool) : base(pool)
        {
            Name = "help [command]";
            ArgumentDescription = "Help (string value), like [help show].";
        }

        public override bool Execute(params string[] param)
        {
            try
            {
                if (param.Length! != 1)
                {
                    PrintArgumentTip();
                    return false;
                }

                return HelpCmd(param.First());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public override string ToString()
        {
            return $"\t{Name} - shows all parameters that spesified command can take.";
        }

        private bool HelpCmd(string arg)
        {
            try
            {
                if (!_pool.Pool.ContainsKey(arg))
                {
                    PrintArgumentTip();
                    return false;
                }

                _pool.Pool[arg].Values.First().PrintBaseToString();

                foreach (var par in _pool.Pool[arg].Values)
                {
                    Console.WriteLine(par.ToString());
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
