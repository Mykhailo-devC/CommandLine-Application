using CommandLine_App.Commands;
using CommandLine_App.Pools;
using CommandLine_App.ProcessService;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CommandLine_App.GlobalCommands.KillCommandChildren
{
    public class KillMemoryCommand : KillCommand
    {
        public KillMemoryCommand(ProcessWrapper wrapper) : base(wrapper)
        {
            Name += CommandChildrenType.memory.ToString();
            ArgumentDescription = "Kill memory (int value), like [kill memory 200]." +
                "\nKill memory (int start, int end), like [kill memory 500 1000].\n";
        }
        public override bool Execute(params string[] param)
        {
            try
            {
                if (param.Length == 1)
                {
                    KillByMemory(int.Parse(param[0]));
                    return true;
                }
                if (param.Length == 2)
                {
                    KillByMemory(int.Parse(param[0]), int.Parse(param[1]));
                    return true;
                }

                Log.Warning($"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}] Incorrect parameters! [parameters = {param}]");
                PrintArgumentTip();
                return false;
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
            return $"\t'{Name}' [memory_value] - stops all running processes with specified memory.";
        }
        private void KillByMemory(int arg)
        {
            var processes = _wrapper.GetProcesses().OrderBy(e => e.ProcessName).Where(e => e.PrivateMemorySize64 / 1024 == arg);

            if (processes.Count() == 0)
            {
                Log.Warning($"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}] No existing processes with using {arg}/Kb of memory!");
                Console.WriteLine("No existing processes with using {0}/Kb of memory!", arg);
            }
            else
            {
                Log.Information($"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}] finished successfully!");
                foreach (var process in processes)
                {
                    Console.WriteLine($"{process.ProcessName} was stopped!");
                    _wrapper.Kill(process);
                }
            }
        }

        private void KillByMemory(int start, int end)
        {
            var processes = _wrapper.GetProcesses().OrderBy(e => e.ProcessName).Where(e => e.PrivateMemorySize64 / 1024 > start && e.PrivateMemorySize64 / 1024 < end);

            if (processes.Count() == 0)
            {
                Log.Warning($"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}] No existing processes with using {start}-{end}/Kb of memory!");
                Console.WriteLine("No existing processes with using memory between {0}/Kb and {1}/Kb!", start, end);
            }
            else
            {
                Log.Information($"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}] finished successfully!");
                foreach (var process in processes)
                {
                    Console.WriteLine($"{process.ProcessName} was stopped!");
                    _wrapper.Kill(process);
                }
            }
        }
    }
}
