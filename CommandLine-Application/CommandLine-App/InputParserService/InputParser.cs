using CommandLine_App.Abstraction;
using CommandLine_App.HelperService;
using CommandLine_App.Pools;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CommandLine_App.InputValidatorService
{
    public class InputParser : IInputParser
    {
        private IHelper _helper;
        public InputParser()
        {
            _helper = new Helper();

        }
        public bool TryParseUserInput(List<string> userInput, out CommandType? Command, out CommandChildrenType? CommandChild, out string[] Arguments)
        {
            Command = null;
            CommandChild = null;
            Arguments = null;

            try
            {
                if (userInput.Count == 0)
                {
                    Log.Warning("User inputs empty string!");
                    _helper.StandartHelp();
                    return false;
                }

                if (userInput.FirstOrDefault() == "help")
                {
                    Log.Information($"User input help command, [input = '{userInput}']");
                    _helper.Help(userInput);
                    return false;
                }

                Arguments = userInput.Skip(2).ToArray();

                if (TryParseCommand(userInput[0], out Command))
                {
                    if (TryParseChildCommand(userInput[1], out CommandChild))
                    {
                        Log.Information("User inputs valid commands!, [input = '{0}']", userInput);
                        switch (CommandChild)
                        {
                            case CommandChildrenType.Memory: return ValidateMemoryChildAguments(Arguments);
                            case CommandChildrenType.Pid: return ValidatePidChildArguments(Arguments);
                            case CommandChildrenType.Name: return ValidateNameChildArguments(Arguments);
                            case CommandChildrenType.All: return ValidateAllChildArguments(Arguments);
                            default: return false;
                        }
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
                Log.Error(ex, "[Class:{0}][Method:{1}][Input = {2}]", this.GetType(), MethodBase.GetCurrentMethod().Name, userInput);
                return false;
            }
        }

        private bool ValidateAllChildArguments(params string[] args)
        {
            if (!args.Any())
            {
                return true;
            }
            else
            {
                Log.Warning("[Class:{0}][Method:{1}] Incorrect arguments! [arguments = {2}]", this.GetType(), MethodBase.GetCurrentMethod().Name, args);
                return false;
            }
        }

        private bool TryParseCommand(string command, out CommandType? Command)
        {
            if(Enum.TryParse(command, true, out CommandType ParsedCommand))
            {
                Command = ParsedCommand;
                return true;
            }
            else
            {
                Log.Warning("User inputs incorrect command, [input = '{0}']", command);
                Command = null;
                return false;
            }
        }

        private bool TryParseChildCommand(string child, out CommandChildrenType? CommandChild)
        {
            if (Enum.TryParse(child, true, out CommandChildrenType ParsedCommandChild))
            {
                CommandChild = ParsedCommandChild; 
                return true;
            }
            else
            {
                Log.Warning("User inputs incorrect command, [input = '{0}']", child);
                CommandChild = null;
                return false;
            }
        }

        private bool ValidateNameChildArguments(params string[] args)
        {
            if(args.Length == 1)
            {
                return true;
            }
            else
            {
                Log.Warning("[Class:{0}][Method:{1}] Incorrect arguments! [arguments = {2}]", this.GetType(), MethodBase.GetCurrentMethod().Name, args);
                return false;
            }
        }

        private bool ValidatePidChildArguments(params string[] args)
        {
            if (args.Length == 1 && int.TryParse(args[0], out _))
            {
                return true;
            }
            else
            {
                Log.Warning("[Class:{0}][Method:{1}] Incorrect arguments! [arguments = {2}]", this.GetType(), MethodBase.GetCurrentMethod().Name, args);
                return false;
            }
        }

        private bool ValidateMemoryChildAguments(params string[] args)
        {
            if (args.Length == 1 && int.TryParse(args[0], out _))
            {
                return true;
            }
            else if(args.Length == 2 && int.TryParse(args[0], out _) && int.TryParse(args[1], out _))
            {
                return true;
            }
            else
            {
                Log.Warning("[Class:{0}][Method:{1}] Incorrect arguments! [arguments = {2}]", this.GetType(), MethodBase.GetCurrentMethod().Name, args);
                return false;
            }
        }

    }
}
