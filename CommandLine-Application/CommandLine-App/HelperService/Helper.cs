using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using CommandLine_App.Abstraction;
using CommandLine_App.Pools;
using Serilog;

namespace CommandLine_App.HelperService
{
    public class Helper : IHelper
    {
        public void StandartHelp()
        {
            Log.Information($"{MethodBase.GetCurrentMethod().Name} was activated!");

            Console.WriteLine("Please type 'help' to see more " +
                "information about avalable commands" +
                "and thier parameters.");
        }

        public void HelpChooseCommand(string param)
        {
            Log.Information($"{MethodBase.GetCurrentMethod().Name} was activated!");

            foreach (var name in Enum.GetNames(typeof(CommandType)))
            {
                if (name.StartsWith(param))
                {
                    Console.WriteLine($"Maybe you mean '{name}' command?");
                }
            }

            Console.WriteLine("Please type 'help' command to see existing commands.");
        }

        public void HelpChooseParameter(string param)
        {
            Log.Information($"{MethodBase.GetCurrentMethod().Name} was activated!");

            foreach (var name in Enum.GetNames(typeof(CommandChildrenType)))
            {
                if (name.StartsWith(param))
                {
                    Console.WriteLine($"Maybe you mean '{name}' parameter?");
                }
            }

            Console.WriteLine("Please type 'help' command to see existing parameters for this command.");
        }
    }
}
