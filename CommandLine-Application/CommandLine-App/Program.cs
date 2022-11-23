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
            var userInput = Console.ReadLine().Split(" ").ToList();

            if (CommandPool.Pool.ContainsKey(userInput[0]))
            {
                CommandPool.Pool[userInput[0]].Execute(userInput.GetRange(1, userInput.Count - 1));
            }
            else
            {
                Helper.HelpChooseCommand(userInput[0]);
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
