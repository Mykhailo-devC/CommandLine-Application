using CommandLine_App.CommandEnum;
using CommandLine_App.Utilities.Implementations;
using Serilog;
using System;
using System.Reflection;

namespace CommandLine_App.GlobalCommands.ServiceCommandParameters.ServiceWatchCommands
{
    public abstract class ServiceWatch : ServiceCommand
    {
        protected Watch _watch;
        new public static ServiceRepository _repository;
        public ServiceWatch()
        {
            _repository = base._repository;
            _watch = ServiceWatchSingleton.Instance;
        }
        public abstract override bool Execute(params string[] param);

    }
}
