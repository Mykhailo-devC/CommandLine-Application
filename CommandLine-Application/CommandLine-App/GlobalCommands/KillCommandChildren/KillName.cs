using CommandLine_App.Abstraction;
using CommandLine_App.Commands;
using CommandLine_App.Pools;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CommandLine_App.GlobalCommands.KillCommandChildren
{
    public class KillName : Kill
    {
        public KillName()
        {
            ArgumentDescription = "Kill name (string value), like [kill name firefox].\n";
        }
        public override bool Execute(params string[] param)
        {
            try
            {
                KillByName(param[0]);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[Class:{0}][Method:{1}][Parameters = {2}]", this.GetType(), MethodBase.GetCurrentMethod().Name, param);
                return false;
            }
        }

        public new string ToString()
        {
            return $"\tCommand '{this.GetType().Name.ToLower().Insert(4, " ")}' [name_value] - stops all running processes with specified name.";
        }
        private void KillByName(string arg)
        {
            var processes = _processWrapper.GetProcessesByName(arg).OrderBy(e => e.Id);

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
                    _processWrapper.Kill(process);
                }
            }
        }
    }
}
