using CommandLine_App.Abstraction;

namespace CommandLine_App.Commands
{
    public abstract class Kill : Command
    {
        public override abstract bool Execute(params string[] param);
    }
}
