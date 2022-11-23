using CommandLine_App.Abstraction;
using CommandLine_App.Parameters;
using CommandLine_App.Parametrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.Commands
{
    public class RefreshCommand : Command
    {
        public override string Name { get; set; }
        public override List<Parameter> Parametrs { get; set; }
        public RefreshCommand()
        {
            Name = "refresh";
            Parametrs = new List<Parameter>()
            {
                new AllParam(),
                new NameParam(),
                new PidParam(),
            };
        }

        public override bool Execute(List<string> param)
        {
            foreach (var p in Parametrs)
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
                    return RefreshAll(args);
                }

                else if (Parametrs[2].NamePool.Contains(par))
                {
                    return RefreshByName(args);
                }

                else if (Parametrs[3].NamePool.Contains(par))
                {
                    return RefreshByPID(args);
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
            str.Append($"\nCommand '{Name}' - refresh running processes.\nParameters:");
            foreach (var p in Parametrs)
            {
                str.Append($"\n\t{Name} {p.Name} [your_argument] - {Name} ");
                str.Append(p.ToString());
            }
            return str.ToString();
        }

        private bool RefreshAll(string arg)
        {
            return true;
        }

        private bool RefreshByName(string arg)
        {
            return true;
        }

        private bool RefreshByPID(string arg)
        {
            return true;
        }
    }
}
