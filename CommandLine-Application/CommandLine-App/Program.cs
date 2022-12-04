using CommandLine_App.Abstraction;
using CommandLine_App.Commands;
using CommandLine_App.Factory;
using CommandLine_App.HelperService;
using CommandLine_App.InputValidatorService;
using CommandLine_App.Pools;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace CommandLine_App
{
    public class Program
    {
        private static IInputValidator _validator = new InputValidator();
        public const string LOGGING_PATH = "\\Logging\\log.txt";
        public static void Main(string[] args)
        {
            LoggerSetup();
            
            var userInput = Console.ReadLine().Split(" ").ToList();

            if (!_validator.IsValid(userInput))
            {
                return;
            }

            var _factory = new CommandFactory();
            var command =_factory.GetCommand(userInput);

            if(command != null)
            {
                if (command.Name.StartsWith("help"))
                {
                    userInput.RemoveRange(0, 1);
                }
                else
                {
                    userInput.RemoveRange(0, 2);
                }

                Log.Information("Command '{0}' has been created", command.Name);
                command.Execute(userInput.ToArray());
            }
            Log.Information("Program finish working.\n");
            Log.CloseAndFlush();
            
            #region
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

        private static void LoggerSetup()
        {
            var flow = Guid.NewGuid();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + LOGGING_PATH,
                    outputTemplate: $"|Flow:{flow}| " + "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

    }
}
