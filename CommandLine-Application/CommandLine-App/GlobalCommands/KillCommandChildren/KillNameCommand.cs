using CommandLine_App.Commands;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CommandLine_App.GlobalCommands.KillCommandChildren
{
    public class KillNameCommand : KillCommand
    {
        public new string Name { get; set; }
        public override string ArgumentDescription { get; set; }
        public KillNameCommand()
        {
            Name = "kill name";
            ArgumentDescription = "Kill name (string value)," +
                "like [kill name firefox].\n";
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

                return KillByName(param.First());
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
            return $"\t'{Name}' [name_value] - stops all running processes with specified name.";
        }
        private bool KillByName(string arg)
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
                    Console.WriteLine($"{processes.First().ProcessName} was stopped!");
                    foreach (var process in processes)
                    {
                        process.Kill();
                    }
                }

                Log.Information("[{0}] Execute has been finished successfully!", this.GetType());
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[{0}] Exeption has been thrown from KillName!", this.GetType());
                return false;
            }
        }
    }
}
