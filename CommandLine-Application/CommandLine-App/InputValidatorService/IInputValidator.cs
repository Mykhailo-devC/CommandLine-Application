using CommandLine_App.Pools;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.InputValidatorService
{
    public interface IInputValidator
    {
        public bool IsValid(List<string> userInput);
        /*public void ParseInput(List<string> userInput,
                               out CommandType firstCommand,
                               out CommandChildrenType secondCommand,
                               out string[] arguments);*/
    }
}
