using CommandLine_App.Abstraction;
using CommandLine_App.Parameters;
using CommandLine_App.Parametrs;
using System;
using System.Collections.Generic;
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

                //execute helper class
                return false;
            }

            var par = param[0];
            var args = param[1];

            try
            {
                if (Parametrs[0].NamePool.Contains(par))
                {
                    return ShowAll(args);
                }

                else if (Parametrs[1].NamePool.Contains(par))
                {
                    return ShowByMemory(args);
                }

                else if (Parametrs[2].NamePool.Contains(par))
                {
                    return ShowByName(args);
                }

                else if (Parametrs[3].NamePool.Contains(par))
                {
                    return ShowByPID(args);
                }

                else
                {
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
                str.Append($"\n\t{Name} {p.Name} {p.ArgView()} - {Name} ");
                str.Append(p.ToString());
            }
            return str.ToString();
        }

        private bool ShowAll(string arg)
        {
            return true;
        }

        private bool ShowByMemory(string arg)
        {
            return true;
        }

        private bool ShowByName(string arg)
        {
            return true;
        }

        private bool ShowByPID(string arg)
        {
            return true;
        }
    }
}
