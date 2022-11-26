using CommandLine_App.Commands;
using CommandLine_App.HelperService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CommandLine_App.GlobalCommands.ShowCommandChildren
{
    public class ShowPidCommand : ShowCommand
    {
        public new string Name { get; set; }
        public override string ArgumentDescription { get; set; }

        public ShowPidCommand()
        {
            Name = "show pid";
            ArgumentDescription = "Show pid (int value), " +
                "like [show pid 89].\n";
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

                return ShowByPID(int.Parse(param.First()));
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
            return $"\t'{Name}' [pid_value] - shows running process" +
                $" with specified process identifier";
        }

        private bool ShowByPID(int arg)
        {
            try
            {
                var process = Process.GetProcessById(arg);

                Console.WriteLine(ProcessesToString(new List<Process> { process }));

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
