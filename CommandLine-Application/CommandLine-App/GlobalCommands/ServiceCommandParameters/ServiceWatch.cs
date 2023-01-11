using CommandLine_App.Utilities.Implementations;
using Serilog;
using System;
using System.Reflection;

namespace CommandLine_App.GlobalCommands.ServiceCommandParameters
{
    public class ServiceWatch : Service
    {
        private readonly Watch _watch;
        public ServiceWatch()
        {
            _watch = Watch.Instance;
        }
        public override bool Execute(params string[] param)
        {
            try
            {
                if (param[0] == "start")
                {
                    _watch.WatchStart();
                    return true;
                }

                if (param[0] == "stop")
                {
                    _watch.WatchStop();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[Class:{0}][Method:{1}][Parameters = {2}]", GetType().Name, MethodBase.GetCurrentMethod().Name, param);
                return false;
            }
        }

    }
}
