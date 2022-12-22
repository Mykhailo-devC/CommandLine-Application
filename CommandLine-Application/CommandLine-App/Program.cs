using CommandLine_App.Factory;
using CommandLine_App.GlobalCommands;
using CommandLine_App.InputValidatorService;
using CommandLine_App.Logging;
using CommandLine_App.Pools;
using CommandLine_App.Utilities.Implementations;
using CommandLine_App.Utilities.Interfaces;
using Serilog;
using System;
using System.Linq;

namespace CommandLine_App
{
    public class Program
    {
        private static readonly IInputParser _validator = new InputParser();
        private static readonly Help Help = new Help();
        public static void Main(string[] args)
        {
            /*var t1 = new InputParseResult();
            t1.command = CommandType.Kill;
            t1.parameter = ParameterType.All;
            t1.arguments = new string[0];

            var t2 = new InputParseResult();
            t2.command = CommandType.Kill;
            t2.parameter = ParameterType.All;
            t2.arguments = new string[0];*/

            string[] t1 = new string[0];
            string[] t2 = new string[0];

            Console.WriteLine(t1 == t2 || Enumerable.SequenceEqual(t1,t2));
            //w.RemoveService("fjhsdkfhsd");
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
                Log.Information("'{0}' has been created", command.GetType().FullName);
                command.Execute(result.arguments);
            }
            else
            {
                log.Error()
            }
            Log.Information("Program finish working.\n");
            Log.CloseAndFlush();*/
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
