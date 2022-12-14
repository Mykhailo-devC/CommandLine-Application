using CommandLine_App.Pools;
using CommandLine_App.Utilities.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.Utilities.Interfaces
{
    public interface IInputParser
    {
        public bool TryParseUserInput(List<string> userInput, out InputParseResult result);
    }
}
