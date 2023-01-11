using CommandLine_App.Utilities.Implementations;
using Serilog;
using System;
using System.Reflection;


namespace CommandLine_App.GlobalCommands.ServiceCommandParameters
{
    public class ServiceRemove : Service
    {
        public override bool Execute(params string[] param)
        {
            try
            {
                ServiceRepository.RemoveService(param[0]);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[Class:{0}][Method:{1}][Parameters = {2}]", this.GetType().Name, MethodBase.GetCurrentMethod().Name, param);
                return false;
            }
        }
    }
}
