using CommandLine_App.Utilities.Implementations;
using System;

namespace CommandLine_App.Abstraction
{
    public interface Command
    {
        public abstract bool Execute(params string[] param);
    }
}
