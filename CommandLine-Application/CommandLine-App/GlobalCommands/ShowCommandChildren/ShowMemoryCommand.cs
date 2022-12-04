using CommandLine_App.Commands;
using CommandLine_App.HelperService;
using CommandLine_App.Pools;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CommandLine_App.GlobalCommands.ShowCommandChildren
{
    public class ShowMemoryCommand : ShowCommand
    {
        public ShowMemoryCommand()
        {
            Name += CommandChildrenType.memory.ToString();
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

                Log.Warning("[{1}] User inputs incorrect count of parameters, [params = '{0}']", param, this.GetType());
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
                    Log.Warning("[{1}] No existing process with current memory, [arg = '{0}']", arg, this.GetType());
                    Console.WriteLine("No existing processes with using {0}/Kb of memory!", arg);
                }

                Log.Information("[{0}] Execute has been finished successfully!", this.GetType());
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool ShowByMemory(int start, int end)
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
                    Console.WriteLine(ProcessesToString(processes));
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