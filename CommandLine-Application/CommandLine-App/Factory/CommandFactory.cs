using CommandLine_App.Abstraction;
using CommandLine_App.GlobalCommands.KillCommandChildren;
using CommandLine_App.GlobalCommands.RefreshCommandChildren;
using CommandLine_App.GlobalCommands.ServiceCommandParameters;
using CommandLine_App.GlobalCommands.ShowCommandChildren;
using CommandLine_App.GlobalCommands.StartCommandChildren;
using CommandLine_App.Pools;
using Serilog;
using System;
using System.Reflection;

namespace CommandLine_App.Factory
{
    public class CommandFactory
    {
        public Command GetCommand(CommandType? command, ParameterType? parameter)
        {
            try
            {
                switch (command)
                {
                    case CommandType.Show:
                        {
                            switch (parameter)
                            {
                                case ParameterType.All: return new ShowAll();
                                case ParameterType.Name: return new ShowName();
                                case ParameterType.Pid: return new ShowPid();
                                case ParameterType.Memory: return new ShowMemory();

                                default: return null;
                            }
                        }
                    case CommandType.Kill:
                        {
                            switch (parameter)
                            {
                                case ParameterType.Name: return new KillName();
                                case ParameterType.Pid: return new KillPid();
                                case ParameterType.Memory: return new KillMemory();

                                default: return null;
                            }
                        }
                    case CommandType.Start:
                        {
                            switch (parameter)
                            {
                                case ParameterType.Name: return new StartName();

                                default: return null;
                            }
                        }
                    case CommandType.Refresh:
                        {
                            switch (parameter)
                            {
                                case ParameterType.Name: return new RefreshName();
                                case ParameterType.Pid: return new RefreshPid();

                                default: return null;
                            }
                        }
                    case CommandType.Service:
                        {
                            switch (parameter)
                            {
                                case ParameterType.Add: return new ServiceAdd();
                                case ParameterType.Remove: return new ServiceRemove();
                                case ParameterType.Update: return new ServiceUpdate();
                                case ParameterType.Watch: return new ServiceWatch();

                                default: return null;
                            }
                        }

                    default: return null;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}][commands = {command} {parameter}]");
                return null;
            }
        }
    }
}
