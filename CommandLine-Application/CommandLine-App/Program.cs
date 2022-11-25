using CommandLine_App.Abstraction;
using CommandLine_App.Commands;
using CommandLine_App.HelperService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandLine_App
{
    public class Program
    {
        public static void Main(string[] args)
        {
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
                
                command.Execute(userInput.ToArray());
            }
            
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
    }
}
