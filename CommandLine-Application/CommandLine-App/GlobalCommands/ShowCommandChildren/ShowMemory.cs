using CommandLine_App.Commands;
using CommandLine_App.Pools;
using CommandLine_App.ProcessService;
using Serilog;
using System;
using System.Linq;
using System.Reflection;


namespace CommandLine_App.GlobalCommands.ShowCommandChildren
{
    public class ShowMemory : Show
    {
        public ShowMemory()
        {
            ArgumentDescription = "Show memory (int value), like [show memory 200]." +
                "\nShow memory (int start, int end), like [show memory 500 1000].\n";
        }
        public override bool Execute(params string[] param)
        {
            try
            {
                if(param.Length == 1)
                {
                    ShowByMemory(int.Parse(param[0]));
                    return true;
                }
                else
                {
                    ShowByMemory(int.Parse(param[0]), int.Parse(param[1]));
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}][parameters = {param}]");
                return false;
            }
        }

        public new string ToString()
        {
            return $"\tCommand '{this.GetType().Name.ToLower().Insert(4, " ")}' [memory_value] - shows all running processes with specified memory.";
        }
        private void ShowByMemory(int arg)
        {
            var processes = _processWrapper.GetProcesses().
                OrderBy(e => e.ProcessName).
                Where(e => e.PrivateMemorySize64 / 1024 == arg);

            if (processes.Count() == 0)
            {
                Log.Warning($"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}] No existing processes with using {arg}/Kb of memory!");
                Console.WriteLine("No existing processes with using {0}/Kb of memory!", arg);
            }
            else
            {
                Log.Information($"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}] finished successfully!");
                Console.WriteLine(ProcessesToString(processes));
            }
        }

        private void ShowByMemory(int start, int end)
        {
            var processes = _processWrapper.GetProcesses().OrderBy(e => e.ProcessName).Where(e => e.PrivateMemorySize64 / 1024 > start && e.PrivateMemorySize64 / 1024 < end);

            if (processes.Count() == 0)
            {
                Log.Warning($"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}] No existing processes with using {start}-{end}/Kb of memory!");
                Console.WriteLine("No existing processes with using memory between {0}/Kb and {1}/Kb!", start, end);
            }
            else
            {
                Log.Information($"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}] finished successfully!");
                Console.WriteLine(ProcessesToString(processes));
            }
        }
    }
}