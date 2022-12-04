using CommandLine_App.Commands;
using CommandLine_App.Pools;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CommandLine_App.GlobalCommands.RefreshCommandChildren
{
    public class RefreshNameCommand : RefreshCommand
    {
        public RefreshNameCommand()
        {
            Name += CommandChildrenType.name.ToString();
            ArgumentDescription = "Refresh name (string value)," +
                "like [refresh name firefox].\n";
        }
        public override bool Execute(params string[] param)
        {
            try
            {
                if (param.Length != 1)
                {
                    Log.Warning("[{1}] User inputs incorrect count of parameters, [params = '{0}']", param, this.GetType());
                    PrintArgumentTip();
                    return false;
                }

                return RefreshByName(param.First());
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}][parameters = {param}]");
                return false;
            }
        }

        public override string ToString()
        {
            return $"\t'{Name}' [name_value] - refresh the running process with specified name.";
        }
        private bool RefreshByName(string arg)
        {
            try
            {
                var processes = Process.GetProcessesByName(arg).OrderBy(e => e.Id);

                if (processes.Count() == 0)
                {
                    Log.Warning("[{1}] No existing process with current name, [arg = '{0}']", arg, this.GetType());
                    Console.WriteLine("No existing processes with '{0}' name!", arg);
                }
                else
                {
                    Console.WriteLine($"{processes.First().ProcessName} was refreshed!");
                    foreach(var process in processes)
                    {
                        process.Refresh();
                    }
                }

                Log.Information("[{0}] Execute has been finished successfully!", this.GetType());
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
