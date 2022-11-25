using CommandLine_App.Commands;
using CommandLine_App.HelperService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CommandLine_App.GlobalCommands.ShowCommandChildren
{
    public class ShowMemoryCommand : ShowCommand
    {
        public new string Name { get; set; }
        public override string ArgumentDescription { get; set; }
        public ShowMemoryCommand()
        {
            Name = "show memory";
            ArgumentDescription = "Show memory (int value), like [show memory 200]." +
                "\nShow memory (int start, int end), like [show memory 500 1000].\n";
        }
        public override bool Execute(params string[] param)
        {
            try
            {
                if (param.Length == 1)
                {
                    return ShowByMemory(int.Parse(param.First()));
                }
                if(param.Length == 2)
                {
                    return ShowByMemory(int.Parse(param.First()), int.Parse(param.Last()));
                }

                PrintArgumentTip();
                return false;
            }
            catch (FormatException)
            {
                PrintArgumentTip();
                return false;
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
            return $"\t'{Name}' [memory_value] - shows all running processes with specified memory.";
        }
        private bool ShowByMemory(int arg)
        {
            try
            {

                var processes = Process.GetProcesses().OrderBy(e => e.ProcessName).Where(e => e.PrivateMemorySize64 / 1024 == arg);

                Console.WriteLine(ProcessesToString(processes));
                if (processes.Count() == 0)
                {
                    Console.WriteLine("No existing processes with using {0}/Kb of memory!", arg);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool ShowByMemory(int start, int end)
        {
            try
            {
                var processes = Process.GetProcesses().OrderBy(e => e.ProcessName).Where(e => e.PrivateMemorySize64 / 1024 > start && e.PrivateMemorySize64 / 1024 < end);

                Console.WriteLine(ProcessesToString(processes));
                if (processes.Count() == 0)
                {
                    Console.WriteLine("No existing processes with using memory between {0}/Kb and {1}/Kb!", start, end);
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