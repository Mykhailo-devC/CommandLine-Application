using CommandLine_App.Commands;
using CommandLine_App.HelperService;
using CommandLine_App.Pools;
using CommandLine_App.ProcessService;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CommandLine_App.GlobalCommands.ShowCommandChildren
{
    public class ShowPid : Show
    { 
        public ShowPid()
        {
            ArgumentDescription = "Show pid (int value), " +
                "like [show pid 89].\n";
        }

        public override bool Execute(params string[] param)
        {
            try
            {
                ShowByPID(int.Parse(param[0]));
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
            return $"\tCommand '{this.GetType().Name.ToLower().Insert(4, " ")}' [pid_value] - shows running process" +
                $" with specified process identifier";
        }

        private void ShowByPID(int arg)
        {
            try
            {
                var process = _processWrapper.GetProcessById(arg);

                Console.WriteLine(ProcessesToString(new List<Process> { process }));

                Log.Information($"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}] finished successfully!");
            }
            catch (ArgumentException ex)
            {
                Log.Warning($"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}] No existing processes with {arg} id!");
                Console.WriteLine("No existing processes with [{0}] id!", arg);
                throw ex;
            }
        }
    }
}
