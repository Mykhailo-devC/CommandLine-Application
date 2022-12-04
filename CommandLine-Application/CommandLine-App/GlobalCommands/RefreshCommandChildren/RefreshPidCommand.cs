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

namespace CommandLine_App.GlobalCommands.RefreshCommandChildren
{
    public class RefreshPidCommand : RefreshCommand
    {
        public RefreshPidCommand()
        {
            Name += CommandChildrenType.pid.ToString();
            ArgumentDescription = "Refresh pid (string value)," +
                "like [refresh name 242].\n";
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

                return RefreshByPID(int.Parse(param.First()));
            }
            catch (FormatException ex)
            {
                Log.Error(ex, $"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}][parameters = {param}]");
                PrintArgumentTip();
                return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}][parameters = {param}]");
                return false;
            }
        }

        public override string ToString()
        {
            return $"\t'{Name}' [pid_value] - refresh running process" +
                $" with specified process identifier";
        }

        private bool RefreshByPID(int arg)
        {
            try
            {
                var process = Process.GetProcessById(arg);

                process.Refresh();

                Console.WriteLine($"{process.ProcessName} was refreshed");

                Log.Information("[{0}] Execute has been finished successfully!", this.GetType());
                return true;
            }
            catch (ArgumentException ex)
            {
                
                Console.WriteLine("No existing processes with [{0}] id!", arg);
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
