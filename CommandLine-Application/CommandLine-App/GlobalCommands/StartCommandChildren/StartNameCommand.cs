using CommandLine_App.Commands;
using CommandLine_App.Pools;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace CommandLine_App.GlobalCommands.StartCommandChildren
{
    public class StartNameCommand : StartCommand
    {
        public StartNameCommand()
        {
            Name += CommandChildrenType.name.ToString();
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

                return StartByName(param.First());
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}][parameters = {param}]");
                return false;
            }
        }

        public override string ToString()
        {
            return $"\t'{Name}' [name_value] - starts the process with specified name.";
        }

        private bool StartByName(string arg)
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
                throw ex;
            }
        }
    }
}
