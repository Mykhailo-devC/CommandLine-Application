using CommandLine_App.Commands;
using CommandLine_App.HelperService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CommandLine_App.GlobalCommands.ShowCommandChildren
{
    public class ShowAllCommand : ShowCommand
    {
        public new string Name { get; set; }
        public override string ArgumentDescription { get; set; }
        public ShowAllCommand()
        {
            Name = "show all";
            ArgumentDescription = "This parameter takes no arguments, like [show all]\n";
        }
        public override bool Execute(params string[] param)
        {
            try
            {
                if (param.Any())
                {
                    PrintArgumentTip();
                    return false;
                }

                return ShowAll();
            }
            catch(Exception ex)
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
            return $"\t'{Name}' - shows all running processes at the execution moment";
        }
        private bool ShowAll()
        {
            try
            {
                var processes = Process.GetProcesses().OrderBy(e => e.ProcessName);

                Console.WriteLine(ProcessesToString(processes));

                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
