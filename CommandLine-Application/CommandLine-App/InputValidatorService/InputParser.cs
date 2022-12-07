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
        public bool IsValid(List<string> userInput, out CommandType? Command, out CommandChildrenType? CommandChild, out string[] Arguments)
        {
            Command = null;
            CommandChild = null;

            try
            {
                if(userInput.FirstOrDefault() == "help")
                {
                    Log.Information($"User input help command, [input = '{userInput}']");
                    _helper.helpCommand.Help(userInput);
                    Arguments = null;
                    return false;
                }

                if (userInput.Count < 2)
                {
                    Log.Warning("User inputs empty string!");
                    _helper.StandartHelp();
                    Arguments = null;
                    return false;
                }

                var onlyArguments = new List<string>(userInput);
                onlyArguments.RemoveRange(0, 2);

                Arguments = onlyArguments.ToArray();

                if (IsCommandValid(userInput[0], out Command))
                {
                    if (IsCommandChildValid(userInput[1], out CommandChild))
                    {
                        Log.Information("User inputs valid commands!, [input = '{0}']", userInput);
                        switch (CommandChild)
                        {
                            case CommandChildrenType.Memory: return ValidateMemoryChildAguments(Arguments);
                            case CommandChildrenType.Pid: return ValidatePidChildArguments(Arguments);
                            case CommandChildrenType.Name: return ValidateNameChildArguments(Arguments);
                            case CommandChildrenType.All: return ValidateAllChildArguments(Arguments);
                            default: return false; ;
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
                Log.Error(ex, $"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}][input = {userInput}]");
                Arguments = null;
                return false;
                
            }
        }

        private bool ValidateAllChildArguments(string[] args)
        {
            if (!args.Any())
            {
                return true;
            }
            else
            {
                Log.Warning($"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}] Incorrect arguments! [arguments = {args}]");
                return false;
            }
        }

        private bool IsCommandValid(string command, out CommandType? Command)
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

        private bool IsCommandChildValid(string child, out CommandChildrenType? CommandChild)
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

        private bool ValidateNameChildArguments(string[] args)
        {
            if(args.Length != 1)
            {
                return true;
            }
            else
            {
                Log.Warning($"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}] Incorrect arguments! [arguments = {args}]");
                return false;
            }
        }

        private bool ValidatePidChildArguments(string[] args)
        {
            if (args.Length != 1 && int.TryParse(args[0], out _))
            {
                return true;
            }
            else
            {
                Log.Warning($"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}] Incorrect arguments! [arguments = {args}]");
                return false;
            }
        }

        private bool ValidateMemoryChildAguments(string[] args)
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
                Log.Warning($"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}] Incorrect arguments! [arguments = {args}]");
                return false;
            }
        }

    }
}
