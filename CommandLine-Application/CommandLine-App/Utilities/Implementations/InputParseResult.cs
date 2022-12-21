using CommandLine_App.Pools;

namespace CommandLine_App.Utilities.Implementations
{
    public class InputParseResult
    {
        public CommandType command = CommandType.Undefined;
        public ParameterType parameter = ParameterType.Undefined;
        public string[] arguments = null;
        public bool IsHelp { get; set; } = false;
    }
}
