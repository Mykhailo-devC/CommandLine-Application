using CommandLine_App.Commands;
using Serilog;
using System;
using System.Diagnostics;
using System.Reflection;

namespace CommandLine_App.GlobalCommands.StartCommandChildren
{
    public class StartName : Start
    {
        public override bool Execute(params string[] param)
        {
            try
            {
                StartByName(param[0]);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[Class:{0}][Method:{1}][Parameters = {2}]", this.GetType().Name, MethodBase.GetCurrentMethod().Name, param);
                return false;
            }
        }
        private void StartByName(string arg)
        {
            var info = new ProcessStartInfo()
            {
                FileName = arg,
            };

            Process.Start(info);

            Log.Information($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] finished successfully!");
            Console.WriteLine($"{arg} was started!");
        }
        
    }
}
