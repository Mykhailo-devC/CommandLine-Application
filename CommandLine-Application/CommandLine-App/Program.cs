using CommandLine_App.Abstraction;
using CommandLine_App.Commands;
using CommandLine_App.Factory;
using CommandLine_App.GlobalCommands.ShowCommandChildren;
using CommandLine_App.HelperService;
using CommandLine_App.InputValidatorService;
using CommandLine_App.Logging;
using CommandLine_App.Pools;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CommandLine_App
{
    public class Program
    {
        private static IInputParser _validator = new InputParser();
        public static void Main(string[] args)
        {
            
            Logger.LoggerSetup();
            var userInput = Console.ReadLine().Split(" ").ToList();

            if (!_validator.IsValid(userInput, out CommandType? Command, out CommandChildrenType? CommandChild, out string[] Arguments)
                || Command == null
                || CommandChild == null)
            {
                return;
            }

            var _factory = new CommandFactory();
            var command =_factory.GetCommand(Command, CommandChild);

            if(command != null)
            {
                Log.Information("'{0}' has been created", command.GetType().FullName);
                command.Execute(Arguments);
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
