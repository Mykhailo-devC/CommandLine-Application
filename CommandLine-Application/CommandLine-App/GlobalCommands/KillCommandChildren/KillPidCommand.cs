using CommandLine_App.Commands;
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
                    PrintArgumentTip();
                    return false;
                }

                return KillByPID(int.Parse(param.First()));
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
            return $"\t'{Name}' [pid_value] - stops running process" +
                $" with specified process identifier";
        }

        private bool KillByPID(int arg)
        {
            try
            {
                var process = Process.GetProcessById(arg);

                process.Kill();

                Console.WriteLine($"{process.ProcessName} was stopped");

                return true;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("No existing processes with [{0}] id!", arg);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
