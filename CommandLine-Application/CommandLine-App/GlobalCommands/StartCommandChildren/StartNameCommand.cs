using CommandLine_App.Commands;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CommandLine_App.GlobalCommands.StartCommandChildren
{
    public class StartNameCommand : StartCommand
    {
        public new string Name { get; set; }
        public override string ArgumentDescription { get; set; }
        public StartNameCommand()
        {
            Name = "start name";
            ArgumentDescription = "Start name (string value)," +
                "like [start name firefox].\n";
        }
        public override bool Execute(params string[] param)
        {
            try
            {
                if (param.Length != 1)
                {
                    Log.Warning("[{1}] User inputs incorrect count of parameters, [params = '{0}']", param, this.GetType());
                    PrintArgumentTip();
                    return false;
                }

                return RefreshByName(param.First());
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[{0}] Exeption has been thrown from Execute!", this.GetType());
                return false;
            }
        }

        public override void PrintBaseToString()
        {
            Console.WriteLine(base.ToString());
        }

        public override string ToString()
        {
            return $"\t'{Name}' [name_value] - starts the process with specified name.";
        }
        private bool RefreshByName(string arg)
        {
            try
            {
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = arg;
                Process.Start(info);

                Log.Information("[{0}] Execute has been finished successfully!", this.GetType());
                Console.WriteLine($"{arg} was started!");
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[{0}] Exeption has been thrown from Execute!", this.GetType());
                return false;
            }
        }
    }
}
