using CommandLine_App.Commands;
using Serilog;
using System;
using System.Linq;
using System.Reflection;

namespace CommandLine_App.GlobalCommands.ShowCommandChildren
{
    public class ShowAll : Show
    {
        public override bool Execute(params string[] param)
        {
            try
            {
                ShowAllProcesses();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[Class:{0}][Method:{1}][Parameters = {2}]", this.GetType(), MethodBase.GetCurrentMethod().Name, param);
                return false;
            }
        }

        private void ShowAllProcesses()
        {
            var processes = _processWrapper.GetProcesses().OrderBy(e => e.ProcessName);

            Console.WriteLine(ProcessesToString(processes));

            Log.Information($"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}] finished successfully!");
        }
    }
}
