using CommandLine_App.Commands;
using CommandLine_App.HelperService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CommandLine_App.GlobalCommands.ShowCommandChildren
{
    public class ShowNameCommand : ShowCommand
    {
        public new string Name { get; set; }
        public override string ArgumentDescription { get; set; }
        public ShowNameCommand()
        {
            Name = "show name";
            ArgumentDescription = "Show name (string value)," +
                "like [show name firefox].\n";
        }
        public override bool Execute(params string[] param)
        {
            try
            {
                if (param.Length != 1)
                {
                    PrintArgumentTip();
                    return false;
                }

                return ShowByName(param.First());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public override void PrintBaseToString()
        {
            Console.WriteLine(base.ToString());
        }

        public override string ToString()
        {
            return $"\t'{Name}' [name_value] - shows all running processes with specified name";
        }
        private bool ShowByName(string arg)
        {
            try
            {
                var processes = Process.GetProcessesByName(arg).OrderBy(e => e.Id);

                Console.WriteLine(ProcessesToString(processes));
                if (processes.Count() == 0)
                {
                    Console.WriteLine("No existing processes with '{0}' name!", arg);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
