using CommandLine_App.Commands;
using Serilog;
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

                Log.Warning("[{1}] User inputs incorrect count of parameters, [params = '{0}']", param, this.GetType());
                PrintArgumentTip();
                return false;
            }
            catch (FormatException ex)
            {
                Log.Error(ex, "[{1}] Exeption has been thrown from Execute! [params = '{0}']",param, this.GetType());
                PrintArgumentTip();
                return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[{1}] Exeption has been thrown from Execute! [params = '{0}']",param, this.GetType());
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
                    Log.Warning("[{1}] No existing process with current memory, [arg = '{0}']", arg, this.GetType());
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

                Log.Information("[{0}] Execute has been finished successfully!", this.GetType());
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool KillByMemory(int start, int end)
        {
            try
            {
                var processes = Process.GetProcesses().OrderBy(e => e.ProcessName).Where(e => e.PrivateMemorySize64 / 1024 > start && e.PrivateMemorySize64 / 1024 < end);

                if (processes.Count() == 0)
                {
                    Log.Warning("[{2}] No existing process with current memory, [arg = '{0}, {1}']", start, end, this.GetType());
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

                Log.Information("[{0}] Execute has been finished successfully!", this.GetType());
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
