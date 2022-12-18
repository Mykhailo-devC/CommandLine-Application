using CommandLine_App.Pools;

namespace CommandLine_App.Utilities.Implementations
{
    public class InputParseResult
    {
        public CommandType Command = CommandType.Undefined;
        public ParameterType Parameter = ParameterType.Undefined;
        public string[] Arguments = null;
        public bool IsHelp { get; set; } = false;
    }
}
