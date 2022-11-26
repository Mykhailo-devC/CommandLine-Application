using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using CommandLine_App.Abstraction;
using CommandLine_App.Pools;

namespace CommandLine_App.HelperService
{
    public class Helper : IHelper
    {
        private readonly IPool<Dictionary<string, Command>> _pool;
        public Helper(IPool<Dictionary<string, Command>> pool)
        {
            _pool = pool;
        }

        public void StandartHelp()
        {
            Console.WriteLine("Please type 'help' to see more " +
                "information about avalable commands" +
                "and thier parameters.");
        }

        public void HelpChooseCommand(string param)
        {
            foreach(var key in _pool.Pool.Keys)
            {
                if (key.StartsWith(param))
                {
                    Console.WriteLine($"Maybe you mean '{key}' command?");
                }
            }
            Console.WriteLine("Please type 'help' command to see existing commands.");
        }

        public void HelpChooseParameter(List<string> param)
        {
            if(param.Count == 1)
            {
                StandartHelp();
                return;
            }

            foreach (var par in _pool.Pool[param[0]].Keys)
            {
                if (par.StartsWith(param[1]))
                {
                    Console.WriteLine($"Maybe you mean '{par}' parameter?");
                }
            }

            Console.WriteLine("Please type 'help' command to see existing parameters for this command.");
        }
    }
}
