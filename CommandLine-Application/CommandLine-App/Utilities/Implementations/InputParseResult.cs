using CommandLine_App.Pools;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.Utilities.Implementations
{
    public class InputParseResult
    {
        public CommandType? Command = null;
        public ParameterType? Parameter = null;
        public string[] Arguments = null;
        public bool IsHelp { get; set; } = false;
    }
}
