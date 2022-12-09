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
    public class ShowAll : Show
    {
        public ShowAll()
        {
            ArgumentDescription = "This parameter takes no arguments, like [show all]\n";
        }
        public override bool Execute(params string[] param)
        {
            try
            {
                ShowAllProcesses();
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
            return $"\tCommand 'show all' - shows all running processes at the execution moment";
        }
        private void ShowAllProcesses()
        {
            var processes = _processWrapper.GetProcesses().OrderBy(e => e.ProcessName);

            Console.WriteLine(ProcessesToString(processes));

            Log.Information($"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}] finished successfully!");
        }
    }
}
