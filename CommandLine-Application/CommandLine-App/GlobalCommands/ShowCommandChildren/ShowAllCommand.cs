using CommandLine_App.Commands;
using CommandLine_App.HelperService;
using Serilog;
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
                    Log.Warning("[{1}] User inputs incorrect count of parameters, [params = '{0}']", param, this.GetType());
                    PrintArgumentTip();
                    return false;
                }

                return ShowAll();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[{1}] Exeption has been thrown from Execute! [params = '{0}']", param, this.GetType());
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

                Log.Information("[{0}] Execute has been finished successfully!", this.GetType());
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
