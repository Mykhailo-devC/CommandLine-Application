using CommandLine_App.Commands;
using CommandLine_App.HelperService;
using CommandLine_App.Pools;
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
            ArgumentDescription = "Show pid (int value), like [show pid 89].\n";
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
                Log.Error(ex, "[Class:{0}][Method:{1}][Parameters = {2}]", this.GetType(), MethodBase.GetCurrentMethod().Name, param);
                return false;
            }
        }

        public new string ToString()
        {
            return $"\tCommand 'show pid' [pid_value] - shows running process with specified process identifier";
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
