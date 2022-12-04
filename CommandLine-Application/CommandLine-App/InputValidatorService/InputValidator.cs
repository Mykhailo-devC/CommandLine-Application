using CommandLine_App.HelperService;
using CommandLine_App.Pools;
using Serilog;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CommandLine_App.InputValidatorService
{
    public class InputValidator : IInputValidator
    {
        public IHelper _helper;
        public InputValidator()
        {
            _helper = new Helper();

        }
        public bool IsValid(List<string> userInput)
        {
            try
            {
                if (userInput.Count == 0)
                {
                    Log.Warning("User inputs empty string!");
                    _helper.StandartHelp();
                    return false;
                }

                if (Enum.IsDefined(typeof(CommandType), userInput[0]))
                {

                    if (Enum.IsDefined(typeof(CommandChildrenType), userInput[1]))
                    {
                        Log.Information("User inputs valid commands!, [input = '{0}']", userInput);
                        return true;
                    }
                    else
                    {
                        Log.Warning("User inputs incorrect command child, [input = '{0}']", userInput);
                        _helper.HelpChooseParameter(userInput[1]);
                        return false;
                    }
                }
                else
                {
                    Log.Warning("User inputs incorrect command, [input = '{0}']", userInput);
                    _helper.HelpChooseCommand(userInput[0]);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}][input = {userInput}]");
                return false;
            }
        }
    }
}
