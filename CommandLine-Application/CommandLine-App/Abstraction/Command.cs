using CommandLine_App.Utilities.Implementations;
using System;

namespace CommandLine_App.Abstraction
{
    public abstract class Command
    {
        protected Command()
        {
            _processWrapper = new ProcessWrapper();
        }
        protected ProcessWrapper _processWrapper { get; set; }
        public abstract bool Execute(params string[] param);
    }
}
