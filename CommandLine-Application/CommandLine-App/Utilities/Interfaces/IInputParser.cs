using CommandLine_App.Utilities.Implementations;
using System.Collections.Generic;

namespace CommandLine_App.Utilities.Interfaces
{
    public interface IInputParser
    {
        public bool TryParseUserInput(List<string> userInput, out InputParseResult result);
    }
}
