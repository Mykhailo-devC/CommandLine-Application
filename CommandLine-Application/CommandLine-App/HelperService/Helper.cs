using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.HelperService
{
    public static class Helper 
    {
        public static void HelpChooseCommand(string param)
        {
            foreach(var key in CommandPool.Pool.Keys)
            {
                if (key.Contains(param))
                {
                    Console.WriteLine($"Maybe you mean '{key}' command?");
                }
            }
            Console.WriteLine("Please type 'help' command to see existing commands.");
        }

        public static void HelpChooseParameter(List<string> param)
        {
            if(param.Count == 1)
            {
                Console.WriteLine("Please type 'help' command to see existing parameters for this command.");
                return;
            }

            foreach (var par in CommandPool.Pool[param[0]].Parametrs)
            {
                foreach(var name in par.NamePool)
                {
                    if (name.Contains(param[1]))
                    {
                        Console.WriteLine($"Maybe you mean '{name}' parameter?");
                    }
                }
                
            }

            Console.WriteLine("Please type 'help' command to see existing parameters for this command.");
        }

        public static void HelpChooseArgument(List<string> param)
        {
            Console.WriteLine(ParameterPool.Pool[param[1]].ArgumentDescription);
            Console.WriteLine($"Please type 'help {param[0]} {param[1]}' command to see existing parameters for this command.");
        }
    }
}
