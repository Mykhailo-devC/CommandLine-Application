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

namespace CommandLine_App.GlobalCommands.RefreshCommandChildren
{
    public class RefreshName : Refresh
    {
        public RefreshName()
        {
            ArgumentDescription = "Refresh name (string value)," +
                "like [refresh name firefox].\n";
        }
        public override bool Execute(params string[] param)
        {
            try
            {
                RefreshByName(param.First());
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
            return $"\tCommand '{this.GetType().Name.ToLower().Insert(7, " ")}' [name_value] - refresh the running process with specified name.";
        }
        private void RefreshByName(string arg)
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
                foreach (var process in processes)
                {
                    _processWrapper.Refresh(process);
                }
            }
        }
    }
}
