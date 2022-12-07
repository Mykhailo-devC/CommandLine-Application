using CommandLine_App.Pools;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.InputValidatorService
{
    public interface IInputParser
    {
        public bool IsValid(List<string> userInput, out CommandType? Command, out CommandChildrenType? CommandChild, out string[] Arguments);
    }
}
