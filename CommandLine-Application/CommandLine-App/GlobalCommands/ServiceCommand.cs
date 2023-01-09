using CommandLine_App.Abstraction;
using CommandLine_App.Utilities.Implementations;

namespace CommandLine_App.GlobalCommands
{
    public abstract class ServiceCommand : Command
    {
        protected readonly ServiceRepository _repository;
        public ServiceCommand() 
        {
            _repository = new ServiceRepository();
        }
        public abstract bool Execute(params string[] param);
    }
}
