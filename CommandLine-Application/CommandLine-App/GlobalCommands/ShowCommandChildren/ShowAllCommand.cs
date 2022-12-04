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
    public class ShowAllCommand : ShowCommand
    {
        public ShowAllCommand(ProcessWrapper wrapper) : base(wrapper)
        {
            Name += CommandChildrenType.all.ToString();
            ArgumentDescription = "This parameter takes no arguments, like [show all]\n";
        }
        public override bool Execute(params string[] param)
        {
            try
            {
                if (param.Any())
                {
                    Log.Warning($"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}] Incorrect parameters! [parameters = {param}]");
                    PrintArgumentTip();
                    return false;
                }

                ShowAll();
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
            return $"\t'{Name}' - shows all running processes at the execution moment";
        }
        private void ShowAll()
        {
            var processes = _wrapper.GetProcesses().OrderBy(e => e.ProcessName);

            Console.WriteLine(ProcessesToString(processes));

            Log.Information($"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}] finished successfully!");
        }
    }
}
