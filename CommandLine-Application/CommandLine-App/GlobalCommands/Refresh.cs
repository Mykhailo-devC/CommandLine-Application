using CommandLine_App.Abstraction;

namespace CommandLine_App.Commands
{
    public abstract class Refresh : Command
    {
        public abstract bool Execute(params string[] param);
    }
}
