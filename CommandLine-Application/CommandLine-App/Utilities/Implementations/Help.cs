using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.Pools
{
    public class Help
    {
        public Dictionary<CommandType, CommandDescription> CommandDescriptions =>
            new Dictionary<CommandType, CommandDescription>()
            {
                { CommandType.Show, new CommandDescription(
                    name: "Show",
                    description: "Command 'show' - shows running processes.",
                    parameters: new Dictionary<ParameterType, string>()
                    {
                        {
                            ParameterType.All,
                            "\tCommand 'show all' - shows all running processes at the execution moment" +
                            "\n\t\tThis parameter takes no arguments, like [show all]\n"
                        },
                        {
                            ParameterType.Memory,
                            "\tCommand 'show memory' [memory_value] - shows all running processes with specified memory." +
                            "\n\t\tShow memory (int value), like [show memory 200]." +
                            "\n\t\tShow memory (int start, int end), like [show memory 500 1000].\n"
                        },
                        {
                            ParameterType.Name,
                            "\tCommand 'show name' [name_value] - shows all running processes with specified name" +
                            "\n\t\tShow name (string value), like [show name firefox].\n"
                        },
                        {
                            ParameterType.Pid,
                            "\tCommand 'show pid' [pid_value] - shows running process with specified process identifier" +
                            "\n\t\tShow pid (int value), like [show pid 89].\n"
                        }
                    }
                    )
                },
                { CommandType.Kill, new CommandDescription(
                    name: "Kill",
                    description: "Command 'kill' - stop specifeied processes.",
                    parameters: new Dictionary<ParameterType, string>()
                    {
                        {
                            ParameterType.Memory,
                            "\tCommand 'kill memory' [memory_value] - stops all running processes with specified memory." +
                            "\n\t\tKill memory (int value), like [kill memory 200]." +
                            "\n\t\tKill memory (int start, int end), like [kill memory 500 1000].\n"
                        },
                        {
                            ParameterType.Name,
                            "\tCommand 'kill name' [name_value] - stops all running processes with specified name." +
                            "\n\t\tKill memory (int start, int end), like [kill memory 500 1000].\n"
                        },
                        {
                            ParameterType.Pid,
                            "\tCommand 'kill pid' [pid_value] - stops running process with specified process identifier" +
                            "\n\t\tKill pid (string value), like [kill name 55].\n"
                        }
                    }
                    )
                },
                { CommandType.Start, new CommandDescription(
                    name: "Start",
                    description: "Command 'start' - starts specifeied processes.",
                    parameters: new Dictionary<ParameterType, string>()
                    {
                        {
                            ParameterType.Name,
                            "\tCommand 'start name' [name_value] - starts the process with specified name." +
                            "\n\t\tStart name (string value), like [start name firefox].\n"
                        },
                    }
                    )
                },
                { CommandType.Refresh, new CommandDescription(
                    name: "Refresh",
                    description: "Command 'refresh' - refresh specifeied processes.",
                    parameters: new Dictionary<ParameterType, string>()
                    {
                        {
                            ParameterType.Name,
                            "\tCommand 'refresh name' [name_value] - refresh the running process with specified name." +
                            "\n\t\tRefresh name (string value), like [refresh name firefox].\n"
                        },
                        {
                            ParameterType.Pid,
                            "\tCommand 'refresh pid' [pid_value] - refresh running process with specified process identifier" +
                            "\n\t\tRefresh pid (string value), like [refresh name 242].\n"
                        }
                    }
                    )
                },
            };

        public override string ToString()
        {
            var builder = new StringBuilder();

            foreach(var command in CommandDescriptions.Values)
            {
                builder.Append(command.ToString());
            }

            return builder.ToString();
        }
        public CommandDescription this[CommandType index]
        {
            get
            {
                if(index == CommandType.Undefined)
                {
                    Console.WriteLine(ToString());
                    return null;
                }

                return CommandDescriptions[index];
            }
        }

    }
}
