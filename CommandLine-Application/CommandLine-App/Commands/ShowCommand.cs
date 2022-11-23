using CommandLine_App.Abstraction;
using CommandLine_App.HelperService;
using CommandLine_App.Parameters;
using CommandLine_App.Parametrs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CommandLine_App.Commands
{
    public class ShowCommand : Command
    {
        public override string Name { get; set; }
        public override List<Parameter> Parametrs { get; set; }
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
        }

        public override bool Execute(List<string> param)
        {
            foreach(var p in Parametrs)
            {
                if (p.NamePool.Contains(param[0]))
                {
                    break;
                }

                Helper.HelpChooseParameter(param);
                return false;
            }

            var par = param[0];
            var args = param.GetRange(1, param.Count - 1);

            try
            {
                if (Parametrs[0].NamePool.Contains(par))
                {
                    return ShowAll();
                }

                else if (Parametrs[1].NamePool.Contains(par))
                {
                    return ShowByMemory();
                }

                else if (Parametrs[2].NamePool.Contains(par))
                {
                    return ShowByName();
                }

                else if (Parametrs[3].NamePool.Contains(par))
                {
                    return ShowByPID();
                }

                else
                {
                    //execute helper class
                    return false;
                }
            }
            catch
            {
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
                var psi = new ProcessStartInfo
                {
                    UseShellExecute = true,
                };
                var processes = Process.GetProcesses();

                foreach(var p in processes)
                {
                    Console.WriteLine("|{0}|{1}|", p.Id, p.ProcessName);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool ShowByMemory()
        {
            return true;
        }

        private bool ShowByName()
        {
            return true;
        }

        private bool ShowByPID()
        {
            return true;
        }
    }
}
