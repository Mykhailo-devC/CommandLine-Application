using CommandLine_App.Abstraction;
using CommandLine_App.Commands;
using CommandLine_App.HelperService;
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
        public static void Main(string[] args)
        {
            LoggerSetup();
            var _factory = new CommandFactory();
            
            var userInput = Console.ReadLine().Split(" ").ToList();

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
                .WriteTo.File("E:\\Projects\\CommandLine-Application\\CommandLine-Application\\CommandLine-App\\Logging\\log.txt",
                    outputTemplate: $"|Flow:{flow}| " + "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
