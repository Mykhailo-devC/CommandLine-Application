using CommandLine_App.Abstraction;
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

                if (userInput.FirstOrDefault() == "help")
                {
                    Log.Information($"User input help command, [input = '{userInput}']");
                    result.IsHelp = true;
                    userInput.RemoveAt(0);

                }

                if (TryParseCommand(userInput.ElementAtOrDefault(0), out result.Command))
                {
                    if (TryParseCommandParameter(userInput.ElementAtOrDefault(1), out result.Parameter))
                    {
                        Log.Information("User inputs valid commands!, [input = '{0}']", userInput);

                        if (result.IsHelp)
                            return true;

                        result.Arguments = userInput.Skip(2).ToArray();

                        switch (result.Parameter)
                        {
                            case ParameterType.Memory: return IsValidMemoryParameterAguments(result.Arguments);
                            case ParameterType.Pid: return IsValidPidParameterArguments(result.Arguments);
                            case ParameterType.Name: return IsValidNameParameterArguments(result.Arguments);
                            case ParameterType.All: return IsValidAllParameterArguments(result.Arguments);
                            default: return false;
                        }
                    }
                    else
                    {
                        if (result.IsHelp && userInput.ElementAtOrDefault(1) == null)
                            return true;

                        Log.Warning("User inputs incorrect command child, [input = '{0}']", userInput);
                        _helper.HelpChooseParameter(userInput.Skip(1).FirstOrDefault());

                        return false;
                    }
                }
                else
                {
                    if (result.IsHelp && userInput.ElementAtOrDefault(0) == null)
                        return true;

                    Log.Warning("User inputs incorrect command, [input = '{0}']", userInput);
                    _helper.HelpChooseCommand(userInput.FirstOrDefault());

                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[Class:{0}][Method:{1}][Input = {2}]", this.GetType(), MethodBase.GetCurrentMethod().Name, userInput);
                return false;
            }
        }

        private bool TryParseCommand(string command, out CommandType Command)
        {
            if(Enum.TryParse(command, true, out CommandType ParsedCommand))
            {
                Command = ParsedCommand;
                return true;
            }
            else
            {
                Command = CommandType.Undefined;
                return false;
            }
        }

        private bool TryParseCommandParameter(string child, out ParameterType CommandChild)
        {
            if (Enum.TryParse(child, true, out ParameterType ParsedCommandChild))
            {
                CommandChild = ParsedCommandChild; 
                return true;
            }
            else
            {
                CommandChild = ParameterType.Undefined;
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
        private bool IsValidNameParameterArguments(params string[] args)
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
