using CommandLine_App.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CommandLine_App.GlobalCommands.KillCommandChildren
{
    public class KillMemoryCommand : KillCommand
    {
        public new string Name { get; set; }
        public override string ArgumentDescription { get; set; }
        public KillMemoryCommand()
        {
            Name = "show memory";
            ArgumentDescription = "Kill memory (int value), like [kill memory 200]." +
                "\nKill memory (int start, int end), like [kill memory 500 1000].\n";
        }
        public override bool Execute(params string[] param)
        {
            try
            {
                if (param.Length == 1)
                {
                    return KillByMemory(int.Parse(param.First()));
                }
                if (param.Length == 2)
                {
                    return KillByMemory(int.Parse(param.First()), int.Parse(param.Last()));
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
            return $"\t'{Name}' [memory_value] - stops all running processes with specified memory.";
        }
        private bool KillByMemory(int arg)
        {
            try
            {

                var processes = Process.GetProcesses().OrderBy(e => e.ProcessName).Where(e => e.PrivateMemorySize64 / 1024 == arg);

                if (processes.Count() == 0)
                {
                    Console.WriteLine("No existing processes with using {0}/Kb of memory!", arg);
                }
                else
                {
                    foreach (var process in processes)
                    {
                        Console.WriteLine($"{process.ProcessName} was stopped!");
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

        private bool KillByMemory(int start, int end)
        {
            try
            {
                var processes = Process.GetProcesses().OrderBy(e => e.ProcessName).Where(e => e.PrivateMemorySize64 / 1024 > start && e.PrivateMemorySize64 / 1024 < end);

                if (processes.Count() == 0)
                {
                    Console.WriteLine("No existing processes with using memory between {0}/Kb and {1}/Kb!", start, end);
                }
                else
                {
                    foreach (var process in processes)
                    {
                        Console.WriteLine($"{process.ProcessName} was stopped!");
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
