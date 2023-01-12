using CommandLine_App.HelperService;
using CommandLine_App.Pools;
using CommandLine_App.Utilities.Implementations;
using CommandLine_App.Utilities.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommandLine_App.InputValidatorService
{
    public class InputParser : IInputParser
    {
        private IHelper _helper;
        public InputParser()
        {
            _helper = new Helper();
        }
        public bool TryParseUserInput(List<string> userInput, out InputParseResult result)
        {
            result = new InputParseResult();

            try
            {
                if (userInput.Count == 0)
                {
                    Log.Warning("User inputs empty string!");
                    _helper.StandartHelp();
                    return false;
                }

                var userCommand = userInput.ElementAtOrDefault(0);

                if (userCommand == "help")
                {
                    Log.Information($"User input help command, [input = '{userInput}']");
                    result.IsHelp = true;
                    userInput.RemoveAt(0);
                    userCommand = userInput.ElementAtOrDefault(0);
                }


                if (TryParseCommand(userCommand, out result.command))
                {
                    var userParameter = userInput.ElementAtOrDefault(1);

                    if (TryParseCommandParameter(userParameter, out result.parameter))
                    {
                        Log.Information("User inputs valid commands!, [input = '{0}']", userInput);

                        if (result.IsHelp)
                            return true;

                        result.arguments = userInput.Skip(2).ToArray();

                        if(result.command == CommandType.Service)
                        {
                            switch (result.parameter)
                            {
                                case ParameterType.Add: return IsPresentSingleStringArgument(result.arguments);
                                case ParameterType.Remove: return IsPresentSingleStringArgument(result.arguments);
                                case ParameterType.Update: return IsValidUpdateParameterArguments(result.arguments);
                                case ParameterType.Watch: return IsValidWatchParameterArguments(result.arguments);
                                case ParameterType.All: return IsValidAllParameterArguments(result.arguments);
                                default: return false;
                            }
                        }

                        switch (result.parameter)
                        {
                            case ParameterType.Memory: return IsValidMemoryParameterAguments(result.arguments);
                            case ParameterType.Pid: return IsValidPidParameterArguments(result.arguments);
                            case ParameterType.Name: return IsPresentSingleStringArgument(result.arguments);
                            case ParameterType.All: return IsValidAllParameterArguments(result.arguments);
                            default: return false;
                        }
                    }
                    else
                    {
                        if (result.IsHelp && userParameter == null)
                            return true;

                        Log.Warning("User inputs incorrect command parameter, [input = '{0}']", userInput);
                        _helper.HelpChooseParameter(userParameter);

                        return false;
                    }
                }
                else
                {
                    if (result.IsHelp && userCommand == null)
                        return true;

                    Log.Warning("User inputs incorrect command, [input = '{0}']", userInput);
                    _helper.HelpChooseCommand(userCommand);

                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[Class:{0}][Method:{1}][Input = {2}]", this.GetType(), MethodBase.GetCurrentMethod().Name, userInput);
                return false;
            }
        }

        private bool IsValidWatchParameterArguments(string[] args)
        {
            if(args.Length == 1 && (args[0] == "start" || args[0] == "stop"))
            {
                return true;
            }
            else
            {
                Log.Warning("[Class:{0}][Method:{1}] Incorrect arguments! [arguments = {2}]", this.GetType().Name, MethodBase.GetCurrentMethod().Name, args);
                return false;
            }
        }

        private bool TryParseCommand(string command, out CommandType parsedCommand)
        {
            if(Enum.TryParse(command, true, out parsedCommand))
            {
                return true;
            }
            else
            {
                parsedCommand = CommandType.Undefined;
                return false;
            }
        }

        private bool TryParseCommandParameter(string parameter, out ParameterType parsedParam)
        {
            if (Enum.TryParse(parameter, true, out parsedParam))
            {
                return true;
            }
            else
            {
                parsedParam = ParameterType.Undefined;
                return false;
            }
        }
        private bool IsValidUpdateParameterArguments(params string[] args)
        {
            if (args.Length == 2 && !string.IsNullOrWhiteSpace(args[0]) && !string.IsNullOrWhiteSpace(args[1]))
            {
                return true;
            }
            else
            {
                Log.Warning("[Class:{0}][Method:{1}] Incorrect arguments! [arguments = {2}]", this.GetType().Name, MethodBase.GetCurrentMethod().Name, args);
                return false;
            }
        }

        private bool IsValidAllParameterArguments(params string[] args)
        {
            if (!args.Any())
            {
                return true;
            }
            else
            {
                Log.Warning("[Class:{0}][Method:{1}] Incorrect arguments! [arguments = {2}]", this.GetType().Name, MethodBase.GetCurrentMethod().Name, args);
                return false;
            }
        }

        private bool IsPresentSingleStringArgument(params string[] args)
        {
            if(args.Length == 1 && !string.IsNullOrWhiteSpace(args[0]))
            {
                return true;
            }
            else
            {
                Log.Warning("[Class:{0}][Method:{1}] Incorrect arguments! [arguments = {2}]", this.GetType(), MethodBase.GetCurrentMethod().Name, args);
                return false;
            }
        }

        private bool IsValidPidParameterArguments(params string[] args)
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

        private bool IsValidMemoryParameterAguments(params string[] args)
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
