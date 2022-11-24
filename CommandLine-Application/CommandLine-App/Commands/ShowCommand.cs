using CommandLine_App.Abstraction;
using CommandLine_App.HelperService;
using CommandLine_App.Parameters;
using CommandLine_App.Parametrs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CommandLine_App.Commands
{
    public class ShowCommand : Command
    {
        public override string Name { get; set; }
        public override List<Parameter> Parametrs { get; set; }
        private List<string> currentCommand { get; set; }
        public ShowCommand()
        {
            Name = "show";
            Parametrs = new List<Parameter>()
            {
                new AllParam(),
                new MemoryParam(), 
                new NameParam(),
                new PidParam(),
            };
            currentCommand = new List<string>() { Name };
        }

        public override bool Execute(List<string> param)
        {
            try
            {
                currentCommand.Add(param[0]);
                var args = param.GetRange(1, param.Count - 1);
                Parameter par = null;

                foreach (var p in Parametrs)
                {
                    if (p.NamePool.Contains(param[0]))
                    {
                        par = p;
                        break;
                    }
                }

                switch (par?.Name)
                {
                    case "all": return ShowAll();
                    case "memory":

                        if (args.Count == 1)
                            return ShowByMemory(args[0]);
                        else
                            return ShowByMemory(args);

                    case "name": return ShowByName(args[0]);
                    case "pid": return ShowByPID(args[0]);
                    default:
                        Helper.HelpChooseParameter(currentCommand);
                        return false;
                }
            }
            catch (NullReferenceException)
            {
                Helper.HelpChooseParameter(currentCommand);
                return false;
            }
            catch (ArgumentOutOfRangeException)
            {
                if (param.Count == 0)
                {
                    Helper.HelpChooseParameter(currentCommand);
                }
                else
                {
                    Helper.HelpChooseArgument(currentCommand);
                }

                return false;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public override string ToString()
        {
            var str = new StringBuilder();
            str.Append($"\nCommand '{Name}' - shows running processes.\nParameters:");
            foreach (var p in Parametrs)
            {
                str.Append($"\n\t{Name} {p.Name} [your_argument] - {Name} ");
                str.Append(p.ToString());
            }
            return str.ToString();
        }

        private bool ShowAll()
        {
            try
            {
                var processes = Process.GetProcesses().OrderBy(e => e.ProcessName);

                Console.WriteLine(ProcessesToString(processes));

                return true;
            }
            catch
            {
                return false;
            }
            
        }

        private bool ShowByMemory(string arg)
        {
            try
            {
                
                var processes = Process.GetProcesses().OrderBy(e => e.ProcessName).Where(e => e.PrivateMemorySize64 / 1024 == int.Parse(arg));

                Console.WriteLine(ProcessesToString(processes));
                if(processes.Count() == 0)
                {
                    Console.WriteLine("No existing processes with using {0}/Kb of memory!", arg);
                }

                return true;
            }
            catch (FormatException)
            {
                Helper.HelpChooseArgument(currentCommand);
                return false;
            }
            catch
            {
                return false;
            }
        }

        private bool ShowByMemory(List<string> args)
        {
            try
            {
                var processes = Process.GetProcesses().OrderBy(e => e.ProcessName).Where(e => e.PrivateMemorySize64 / 1024 > int.Parse(args[0]) && e.PrivateMemorySize64 / 1024 < int.Parse(args[1]));

                Console.WriteLine(ProcessesToString(processes));
                if (processes.Count() == 0)
                {
                    Console.WriteLine("No existing processes with using memory between {0}/Kb and {1}/Kb!", args[0], args[1]);
                }

                return true;
            }
            catch (FormatException)
            {
                Helper.HelpChooseArgument(currentCommand);
                return false;
            }
            catch
            {
                return false;
            }
        }

        private bool ShowByName(string arg)
        {
            try
            {
                var processes = Process.GetProcessesByName(arg).OrderBy(e => e.Id);

                Console.WriteLine(ProcessesToString(processes));
                if (processes.Count() == 0)
                {
                    Console.WriteLine("No existing processes with '{0}' name!", arg);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool ShowByPID(string arg)
        {
            try
            {
                var processes = Process.GetProcesses().Where(e => e.Id == int.Parse(arg));

                Console.WriteLine(ProcessesToString(processes));
                if (processes.Count() == 0)
                {
                    Console.WriteLine("No existing processes with [{0}] id!", arg);
                }

                return true;
            }
            catch (FormatException)
            {
                Helper.HelpChooseArgument(currentCommand);
                return false;
            }
            catch
            {
                return false;
            }
        }

        private string ProcessesToString(IEnumerable<Process> processes)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(new String('-', 54));
            sb.Append(String.Format("\n|{0,-5}|{1,-36}|{2, 6}/Kb|\n", "Id", "Process Name", "Memory"));
            sb.Append(new String('-', 54));

            foreach (var p in processes)
            {
                sb.Append(String.Format("\n|{0,-5}|{1,-36}|{2, 6}/Kb|", p.Id, p.ProcessName, p.PrivateMemorySize64 / 1024));
            }

            return sb.ToString();
        }
    }
}
