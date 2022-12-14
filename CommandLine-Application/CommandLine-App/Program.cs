
using CommandLine_App.Factory;
using CommandLine_App.InputValidatorService;
using CommandLine_App.Logging;
using CommandLine_App.Pools;
using CommandLine_App.Utilities.Implementations;
using CommandLine_App.Utilities.Interfaces;
using Serilog;
using System;
using System.Linq;
using System.Reflection;

namespace CommandLine_App
{
    public class Program
    {
        private static readonly IInputParser _validator = new InputParser();
        private static readonly Help Help = new Help();
        public static void Main(string[] args)
        { 
            Logger.LoggerSetup();
            var userInput = Console.ReadLine().Split(" ").ToList();

            if (!_validator.TryParseUserInput(userInput, out InputParseResult result))
            { 
                return;
            }

            if (result.IsHelp)
            {
                if (result.Command != null)
                {
                    if (result.Parameter != null)
                    {
                        Console.WriteLine(Help[result.Command][result.Parameter]);
                    }
                    else
                    {
                        Console.WriteLine(Help[result.Command]);
                    }
                }
                else
                {
                    Console.WriteLine(Help);
                }

                return;
            }

            var _factory = new CommandFactory();
            var command =_factory.GetCommand(result.Command, result.Parameter);

            if(command != null)
            {
                Log.Information("'{0}' has been created", command.GetType().FullName);
                command.Execute(result.Arguments);
            }
            Log.Information("Program finish working.\n");
            Log.CloseAndFlush();
            #region
            /*
            var commands = typeof(Command)
                .Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Command)) && !t.IsAbstract)
                .Select(t => (Command)Activator.CreateInstance(t));

            foreach(var c in commands)
            {
                var target = c.ToString();
                Console.WriteLine(target);
            };
            */
            /*while (true)
            {
                var userInput = Console.ReadLine();

                if (CommandPool.Pool.Keys.Contains(userInput))
                {
                    CommandPool.Pool[userInput].Execute(null);
                }
                else if(userInput == "exit")
                {
                    break;
                }
                else
                {
                    //execute helper class
                    Helper.HelpChooseCommand(userInput);
                }
            }*/
            #endregion
        }



    }
}
