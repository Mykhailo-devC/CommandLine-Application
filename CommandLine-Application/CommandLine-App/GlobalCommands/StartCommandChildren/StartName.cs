using CommandLine_App.Abstraction;
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
    public class StartName : Start
    {
        public StartName()
        {
            ArgumentDescription = "Start name (string value), like [start name firefox].\n";
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
                Log.Error(ex, "[Class:{0}][Method:{1}][Parameters = {2}]", this.GetType(), MethodBase.GetCurrentMethod().Name, param);
                return false;
            }
        }

        public new string ToString()
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
