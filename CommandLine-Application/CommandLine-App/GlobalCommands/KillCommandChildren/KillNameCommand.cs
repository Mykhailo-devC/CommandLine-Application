using CommandLine_App.Commands;
using CommandLine_App.Pools;
using CommandLine_App.ProcessService;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CommandLine_App.GlobalCommands.KillCommandChildren
{
    public class KillNameCommand : KillCommand
    {
        public KillNameCommand(ProcessWrapper wrapper) : base(wrapper)
        {
            Name += CommandChildrenType.name.ToString();
            ArgumentDescription = "Kill name (string value)," +
                "like [kill name firefox].\n";
        }
        public override bool Execute(params string[] param)
        {
            try
            {
                if (param.Length != 1)
                {
                    Log.Warning($"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}] Incorrect parameters! [parameters = {param}]");
                    PrintArgumentTip();
                    return false;
                }

                KillByName(param[0]);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}][parameters = {param}]");
                return false;
            }
        }

        public override string ToString()
        {
            return $"\t'{Name}' [name_value] - stops all running processes with specified name.";
        }
        private void KillByName(string arg)
        {
            var processes = _wrapper.GetProcessesByName(arg).OrderBy(e => e.Id);

            if (processes.Count() == 0)
            {
                Log.Warning($"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}] No existing processes with {arg} name!");
                Console.WriteLine("No existing processes with '{0}' name!", arg);
            }
            else
            {
                Log.Information($"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}] finished successfully!");
                Console.WriteLine($"{processes.First().ProcessName} was stopped!");
                foreach (var process in processes)
                {
                    _wrapper.Kill(process);
                }
            }
        }
    }
}
