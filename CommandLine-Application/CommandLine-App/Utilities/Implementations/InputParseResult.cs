using CommandLine_App.CommandEnum;
using CommandLine_App.Pools;
using System.Linq;

namespace CommandLine_App.Utilities.Implementations
{
    public class InputParseResult
    {
        public CommandType command = CommandType.Undefined;
        public ParameterType parameter = ParameterType.Undefined;
        public WatchMode watchMode = WatchMode.Undefined;
        public string[] arguments = null;
        public bool IsHelp { get; set; } = false;

        public override bool Equals(object obj)
        {
            if(obj is InputParseResult input)
            {
                if(input.command == command &&
                   input.parameter == parameter &&
                   (input.arguments == arguments || Enumerable.SequenceEqual(input.arguments, arguments)) &&
                   input.IsHelp == IsHelp)
                    return true;
            }

            return false;
        }
    }
}
