using CommandLine_App.Commands;
using Serilog;
using System;
using System.Diagnostics;
using System.Reflection;

namespace CommandLine_App.GlobalCommands.RefreshCommandChildren
{
    public class RefreshPid : Refresh
    {
        public override bool Execute(params string[] param)
        {
            try
            {
                RefreshByPID(int.Parse(param[0]));
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[Class:{0}][Method:{1}][Parameters = {2}]", this.GetType().Name, MethodBase.GetCurrentMethod().Name, param);
                return false;
            }
        }

        private void RefreshByPID(int arg)
        {
            try
            {
                var process = Process.GetProcessById(arg);

                process.Refresh();

                Console.WriteLine($"{process.ProcessName} was refreshed");

                Log.Information($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] finished successfully!");
            }
            catch (ArgumentException ex)
            {
                Log.Warning($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] No existing processes with {arg} id!");
                Console.WriteLine("No existing processes with [{0}] id!", arg);
                throw ex;
            }
        }
    }
}
