using CommandLine_App.Commands;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CommandLine_App.GlobalCommands.KillCommandChildren
{
    public class KillPidCommand : KillCommand
    {
        public new string Name { get; set; }
        public override string ArgumentDescription { get; set; }
        public KillPidCommand()
        {
            Name = "kill pid";
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
            catch (Exception ex)
            {
                Log.Error(ex, "[{0}] Exeption has been thrown from Execute!", this.GetType());
                return false;
            }
        }

        public override void PrintBaseToString()
        {
            Console.WriteLine(base.ToString());
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
            catch (ArgumentException)
            {
                Log.Warning("[{1}] No existing process with current id, [arg = '{0}']", arg, this.GetType());
                Console.WriteLine("No existing processes with [{0}] id!", arg);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[{0}] Exeption has been thrown from KillPid!", this.GetType());
                return false;
            }
        }
    }
}
