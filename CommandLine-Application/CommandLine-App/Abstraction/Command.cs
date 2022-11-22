using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.Abstraction
{
    public abstract class Command
    {
        public abstract string Name { get; set; }
        public abstract List<Parameter> Parametrs { get; set; }
        public abstract bool Execute(List<string> param);
        public abstract override string ToString();

    }
}
