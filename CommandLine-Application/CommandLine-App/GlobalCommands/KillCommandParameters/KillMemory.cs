using CommandLine_App.Commands;
using Serilog;
using System;
using System.Linq;
using System.Reflection;

namespace CommandLine_App.GlobalCommands.KillCommandChildren
{
    public class KillMemory : Kill
    {
        public override bool Execute(params string[] param)
        {
            try
            {
                if (param.Length == 1)
                {
                    KillByMemory(int.Parse(param[0]));
                    return true;
                }
                else
                {
                    KillByMemory(int.Parse(param[0]), int.Parse(param[1]));
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[Class:{0}][Method:{1}][Parameters = {2}]", this.GetType().Name, MethodBase.GetCurrentMethod().Name, param);
                return false;
            }
        }

        private void KillByMemory(int arg)
        {
            var processes = _processWrapper.GetProcesses().OrderBy(e => e.ProcessName).Where(e => e.PrivateMemorySize64 / 1024 == arg);

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
                    _processWrapper.Kill(process);
                }
            }
        }

        private void KillByMemory(int start, int end)
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
                foreach (var process in processes)
                {
                    Console.WriteLine($"{process.ProcessName} was stopped!");
                    _processWrapper.Kill(process);
                }
            }
        }
    }
}
