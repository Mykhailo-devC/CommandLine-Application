using CommandLine_App.Abstraction;
using CommandLine_App.Commands;
using CommandLine_App.HelperService;
using CommandLine_App.Pools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandLine_App.GlobalCommands.HelpCommandChildren
{
    public class HelpParamCommand : HelpCommand
    {
        public new string Name { get; set; }
        public override string ArgumentDescription { get; set; }
        private readonly IHelper _helper;

        public HelpParamCommand(IPool<Dictionary<string, Command>> pool, IHelper helper) : base(pool)
        {
            Name = "help [command] [parameter]";
            ArgumentDescription = "Help (string value) (string vlue), like [help show memory].";
            _helper = helper;
        }

        public override bool Execute(params string[] param)
        {
            try
            {
                if (param.Length != 2)
                {
                    PrintArgumentTip();
                    return false;
                }

                return HelpParam(param.First(), param.Last());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public override string ToString()
        {
            return $"\t'{Name}' - shows type of arguments that can take specified command.";
        }

        private bool HelpParam(string com, string par)
        {
            try
            {
                if (!_pool.Pool.ContainsKey(com))
                {
                    PrintArgumentTip();
                    _helper.HelpChooseCommand(com);
                    return false;
                }

                if (!_pool.Pool[com].ContainsKey(par))
                {
                    PrintArgumentTip();
                    _helper.HelpChooseParameter(new List<string> { com, par });
                    return false;
                }

                _pool.Pool[com][par].PrintArgumentTip();
                Console.WriteLine(_pool.Pool[com][par].ToString());

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
