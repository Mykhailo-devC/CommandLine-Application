using CommandLine_App.Abstraction;

namespace CommandLine_App.GlobalCommands
{
    public abstract class Service: Command
    {
        public abstract bool Execute(params string[] param);
    }
}
