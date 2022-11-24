using CommandLine_App.Abstraction;
using CommandLine_App.HelperService;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.Commands
{
    public class HelpCommand : Command
    {
        public override string Name { get; set; }
        public override List<Parameter> Parametrs { get; set; }

        public HelpCommand()
        {
            Name = "help";
            Parametrs = new List<Parameter>();
        }

        public override bool Execute(List<string> param)
        {
            try
            {
                switch (param.Count)
                {
                    case 0: return Help();
                    case 1: return HelpCmd(param[0]);
                    case 2: return HelpParam(param);
                    default:
                        Console.WriteLine("Too many parameters!");
                        Console.WriteLine(ToString());
                        return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            
        }

        public override string ToString()
        {
            var str = new StringBuilder();
            str.Append($"\nCommand '{Name}' - shows all commands and their parameters.\nParameters:");

            str.Append($"\n\t {Name} - shows all information");
            str.Append($"\n\t {Name} [your_command] - shows information about current comamnd");
            str.Append($"\n\t {Name} [your_command] [your_parameter] - shows information about arguments, that takes current parameter");

            return str.ToString();
        }
        private bool Help()
        {
            try
            {
                foreach (var c in CommandPool.Pool)
                {
                    Console.WriteLine(c.Value.ToString());
                }

                return true;
            }
            catch
            {
                //TODO: Logging
                return false;
            }
        }

        private bool HelpCmd(string param)
        {
            try
            {
                Console.WriteLine(CommandPool.Pool[param].ToString());
                return true;
            }
            catch(KeyNotFoundException)
            {
                Helper.HelpChooseCommand(param);
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        private bool HelpParam(List<string> param)
        {
            try
            {
                Console.WriteLine(CommandPool.Pool[param[0]].Parametrs.Find(e => e.NamePool.Contains(param[1])).ArgumentDescription);
                return true;
            }
            catch(KeyNotFoundException)
            {
                Helper.HelpChooseCommand(param[0]);
                return false;
            }
            catch (NullReferenceException)
            {
                Helper.HelpChooseParameter(param);
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
