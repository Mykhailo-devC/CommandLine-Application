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
    public class KillPidCommand : KillCommand
    {
        public KillPidCommand()
        {
            Name += CommandChildrenType.pid.ToString();
            ArgumentDescription = "Kill pid (string value)," +
                "like [kill name 55].\n";
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

                return KillByPID(int.Parse(param.First()));
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
            return $"\t'{Name}' [pid_value] - stops running process" +
                $" with specified process identifier";
        }

        private bool KillByPID(int arg)
        {
            try
            {
                var process = Process.GetProcessById(arg);

                process.Kill();

                Log.Information("[{0}] Execute has been finished successfully!", this.GetType());
                Console.WriteLine($"{process.ProcessName} was stopped");

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
