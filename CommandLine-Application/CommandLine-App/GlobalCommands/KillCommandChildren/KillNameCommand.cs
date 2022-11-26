using CommandLine_App.Commands;
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
                    PrintArgumentTip();
                    return false;
                }

                return KillByName(param.First());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
