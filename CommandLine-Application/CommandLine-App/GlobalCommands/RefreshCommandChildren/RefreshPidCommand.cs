using CommandLine_App.Abstraction;
using CommandLine_App.Commands;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CommandLine_App.GlobalCommands.RefreshCommandChildren
{
    public class RefreshPidCommand : RefreshCommand
    {
        public new string Name { get; set; }
        public override string ArgumentDescription { get; set; }
        public RefreshPidCommand()
        {
            Name = "refresh pid";
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
                Log.Error(ex, "[{0}] Exeption has been thrown from Execute!", this.GetType());
                PrintArgumentTip();
                return false;
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
            catch (ArgumentException)
            {
                Log.Warning("[{1}] No existing process with current id, [arg = '{0}']", arg, this.GetType());
                Console.WriteLine("No existing processes with [{0}] id!", arg);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[{0}] Exeption has been thrown from RefreshPid!", this.GetType());
                return false;
            }
        }
    }
}
