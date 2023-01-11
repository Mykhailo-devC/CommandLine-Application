using CommandLine_App.Factory;
using CommandLine_App.GlobalCommands;
using CommandLine_App.InputValidatorService;
using CommandLine_App.Logging;
using CommandLine_App.Pools;
using CommandLine_App.Utilities.Implementations;
using CommandLine_App.Utilities.Interfaces;
using Serilog;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace CommandLine_App
{
    public class Program
    {
        private static readonly IInputParser _validator = new InputParser();
        private static readonly Help Help = new Help();
        public static void Main(string[] args)
        {
            #region Single Command Flow
            /*Logger.LoggerSetup();
            var userInput = Console.ReadLine().Split(" ").ToList();

            if (!_validator.TryParseUserInput(userInput, out InputParseResult result))
            { 
                return;
            }

            if (result.IsHelp)
            {
                Console.WriteLine(Help[result.command]?[result.parameter]);
                return;
            }

            var _factory = new CommandFactory();
            var command =_factory.GetCommand(result.command, result.parameter);

            if(command != null)
            {
                Log.Information($"[Method:{MethodBase.GetCurrentMethod().Name}]" + 
                    $"'{command.GetType().FullName}' has been created");
                command.Execute(result.arguments);
            }
            else
            {
                Log.Error($"[Method:{MethodBase.GetCurrentMethod().Name}] Command == null");
            }
            
            Log.Information($"Method:{MethodBase.GetCurrentMethod().Name}]" + 
                        "Program finish working.\n");
            Log.CloseAndFlush();*/
            #endregion

            #region MultiCommand Flow


            Logger.LoggerSetup();
            var _factory = new CommandFactory();

            while (true)
            {
                
                var userInput = Console.ReadLine().Split(" ").ToList();

                if (userInput[0] == "exit")
                {
                    break;
                }

                if (!_validator.TryParseUserInput(userInput, out InputParseResult result))
                {
                    break;
                }

                if (result.IsHelp)
                {
                    Console.WriteLine(Help[result.command]?[result.parameter]);
                    break;
                }


                var command = _factory.GetCommand(result.command, result.parameter);

                if (command != null)
                {
                    Log.Information($"[Method:{MethodBase.GetCurrentMethod().Name}]" +
                        $"'{command.GetType().Name}' has been created");
                    command.Execute(result.arguments);
                }
                else
                {
                    Console.WriteLine("Command == null!");
                    Log.Error($"[Method:{MethodBase.GetCurrentMethod().Name}] Command == null");
                }
            }

            Log.Information($"Method:{MethodBase.GetCurrentMethod().Name}]" +
                            "Program finish working.\n");
            Log.CloseAndFlush();
            #endregion
        }



    }
}
