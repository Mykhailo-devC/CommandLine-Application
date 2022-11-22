using CommandLine_App.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.Commands
{
    public class HelpCommand : Command
    {
        public override string Name { get; set; }
        public override List<Parameter> Parametrs { get; set; }

        public HelpCommand()
        {
            Name = "help";
            Parametrs = new List<Parameter>();
        }

        public override bool Execute(List<string> param)
        {
            throw new NotImplementedException();
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
    }
}
