using CommandLine_App.Abstraction;
using CommandLine_App.Commands;
using CommandLine_App.GlobalCommands.KillCommandChildren;
using CommandLine_App.GlobalCommands.RefreshCommandChildren;
using CommandLine_App.GlobalCommands.ShowCommandChildren;
using CommandLine_App.GlobalCommands.StartCommandChildren;
using CommandLine_App.HelperService;
using CommandLine_App.InputValidatorService;
using CommandLine_App.Pools;
using CommandLine_App.ProcessService;
using Serilog;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CommandLine_App.Factory
{
    public class CommandFactory
    {
        public Command GetCommand(CommandType? Command, CommandChildrenType? CommandChild)
        {
            try
            {
                switch (Command)
                {
                    case CommandType.Show:
                        {
                            switch (CommandChild)
                            {
                                case CommandChildrenType.All: return new ShowAll();
                                case CommandChildrenType.Name: return new ShowName();
                                case CommandChildrenType.Pid: return new ShowPid();
                                case CommandChildrenType.Memory: return new ShowMemory();

                                default: return null;
                            }
                        }
                    case CommandType.Kill:
                        {
                            switch (CommandChild)
                            {
                                case CommandChildrenType.Name: return new KillName();
                                case CommandChildrenType.Pid: return new KillPid();
                                case CommandChildrenType.Memory: return new KillMemory();

                                default: return null;
                            }
                        }
                    case CommandType.Start:
                        {
                            switch (CommandChild)
                            {
                                case CommandChildrenType.Name: return new StartName();

                                default: return null;
                            }
                        }
                    case CommandType.Refresh:
                        {
                            switch (CommandChild)
                            {
                                case CommandChildrenType.Name: return new RefreshName();
                                case CommandChildrenType.Pid: return new RefreshPid();

                                default: return null;
                            }
                        }

                    default: return null;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}][commands = {Command} {CommandChild}]");
                return null;
            }
        }
    }
}
