using CommandLine_App.Abstraction;
using CommandLine_App.Commands;
using CommandLine_App.GlobalCommands.HelpCommandChildren;
using CommandLine_App.GlobalCommands.KillCommandChildren;
using CommandLine_App.GlobalCommands.RefreshCommandChildren;
using CommandLine_App.GlobalCommands.ShowCommandChildren;
using CommandLine_App.GlobalCommands.StartCommandChildren;
using CommandLine_App.HelperService;
using CommandLine_App.Pools;
using Serilog;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CommandLine_App.Factory
{
    public class CommandFactory
    {

        public Command GetCommand(List<string> userInput)
        {
            try
            {
                switch (Enum.Parse<CommandType>(userInput[0]))
                {
                    case CommandType.help:
                        {
                            switch (userInput.Count)
                            {
                                case 0: return new HelpGlobalCommand(new CommandPool());
                                case 1: return new HelpCmdCommand(new CommandPool());
                                case 2: return new HelpParamCommand(new CommandPool(), new Helper());

                                default: return null;
                            }
                        }
                    case CommandType.show:
                        {
                            switch (Enum.Parse<CommandChildrenType>(userInput[1]))
                            {
                                case CommandChildrenType.all: return new ShowAllCommand();
                                case CommandChildrenType.name: return new ShowNameCommand();
                                case CommandChildrenType.pid: return new ShowPidCommand();
                                case CommandChildrenType.memory: return new ShowMemoryCommand();

                                default: return null;
                            }
                        }
                    case CommandType.kill:
                        {
                            switch (Enum.Parse<CommandChildrenType>(userInput[1]))
                            {
                                case CommandChildrenType.name: return new KillNameCommand();
                                case CommandChildrenType.pid: return new KillPidCommand();
                                case CommandChildrenType.memory: return new KillMemoryCommand();

                                default: return null;
                            }
                        }
                    case CommandType.start:
                        {
                            switch (Enum.Parse<CommandChildrenType>(userInput[1]))
                            {
                                case CommandChildrenType.name: return new StartNameCommand();

                                default: return null;
                            }
                        }
                    case CommandType.refresh:
                        {
                            switch (Enum.Parse<CommandChildrenType>(userInput[1]))
                            {
                                case CommandChildrenType.name: return new RefreshNameCommand();
                                case CommandChildrenType.pid: return new RefreshPidCommand();

                                default: return null;
                            }
                        }

                    default: return null;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}][input = {userInput}]");
                return null;
            }
        }
    }
}
