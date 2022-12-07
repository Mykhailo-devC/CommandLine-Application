using CommandLine_App.Commands;
using CommandLine_App.Pools;
using CommandLine_App.ProcessService;
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
    public class StartName : Start
    {
        public StartName()
        {
            ArgumentDescription = "Start name (string value)," +
                "like [start name firefox].\n";
        }
        public override bool Execute(params string[] param)
        {
            try
            {
                StartByName(param[0]);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}][parameters = {param}]");
                return false;
            }
        }

        public override string ToString()
        {
            return $"\tCommand '{this.GetType().Name.ToLower().Insert(5, " ")}' [name_value] - starts the process with specified name.";
        }

        private void StartByName(string arg)
        {
            _processWrapper.Start(arg);

            Log.Information($"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}] finished successfully!");
            Console.WriteLine($"{arg} was started!");
        }
    }
}
