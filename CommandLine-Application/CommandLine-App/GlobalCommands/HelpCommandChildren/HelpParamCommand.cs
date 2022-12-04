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
    public class HelpParamCommand : HelpCommand
    {
        private readonly IHelper _helper;

        public HelpParamCommand(IPool<Dictionary<string, Command>> pool, IHelper helper) : base(pool)
        {
            Name += "[command] [parameter]";
            ArgumentDescription = "Help (string value) (string vlue), like [help show memory].";
            _helper = helper;
        }

        public override bool Execute(params string[] param)
        {
            try
            {
                if (param.Length != 2)
                {
                    Log.Warning("[{1}] User inputs incorrect count of parameters, [params = '{0}']", param, this.GetType());
                    PrintArgumentTip();
                    return false;
                }

                return HelpParam(param.First(), param.Last());
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}][parameters = {param}]");
                return false;
            }
        }

        public override string ToString()
        {
            return $"\t'{Name}' - shows type of arguments that can take specified command.";
        }

        private bool HelpParam(string com, string par)
        {
            if (!_pool.Pool.ContainsKey(com))
            {
                Log.Warning("[{1}] User inputs incorrect command, [com = '{0}']", com, this.GetType());
                PrintArgumentTip();
                _helper.HelpChooseCommand(com);
                return false;
            }

            if (!_pool.Pool[com].ContainsKey(par))
            {
                Log.Warning("[{1}] User inputs incorrect parameters, [params = '{0}']", par, this.GetType());
                PrintArgumentTip();
                _helper.HelpChooseParameter(par);
                return false;
            }

            _pool.Pool[com][par].PrintArgumentTip();
            Console.WriteLine(_pool.Pool[com][par].ToString());

            Log.Information("[{0}] Execute has been finished successfully!", this.GetType());
            return true;
        }
    }
}
