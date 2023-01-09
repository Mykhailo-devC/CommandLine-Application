using Serilog;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CommandLine_App.GlobalCommands.ServiceCommandParameters.ServiceWatchCommands
{
    public class ServiceWatchStop : ServiceWatch
    {
        public override bool Execute(params string[] param)
        {
            try
            {
                _watch.WatchStop();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[Class:{0}][Method:{1}][Parameters = {2}]", GetType().Name, MethodBase.GetCurrentMethod().Name, param);
                return false;
            }
        }
    }
}

