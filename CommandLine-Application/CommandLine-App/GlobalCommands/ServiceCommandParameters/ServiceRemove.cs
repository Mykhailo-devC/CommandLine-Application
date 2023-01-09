using Serilog;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CommandLine_App.GlobalCommands.ServiceCommandParameters
{
    public class ServiceRemove : ServiceCommand
    {
        public override bool Execute(params string[] param)
        {
            try
            {
                _repository.RemoveService(param[0]);
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
