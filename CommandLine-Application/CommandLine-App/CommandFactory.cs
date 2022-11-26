using CommandLine_App.Abstraction;
using CommandLine_App.Commands;
using CommandLine_App.GlobalCommands.HelpCommandChildren;
using CommandLine_App.HelperService;
using CommandLine_App.Pools;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App
{
    public class CommandFactory
    {
        private readonly IPool<Dictionary<string, Command>> _pool;
        private readonly IHelper _helper;

        public CommandFactory()
        {
            _pool = new CommandPool();
            _helper = new Helper(_pool);
        }

        public Command GetCommand(List<string> com)
        {
            try
            {
                if(com.Count == 0)
                {
                    _helper.StandartHelp();
                    return null;
                }

                switch (com[0])
                {
                    case "show":
                        {
                            foreach (var p in _pool.Pool[com[0]].Keys)
                            {
                                if (p == com[1])
                                {
                                    return _pool.Pool[com[0]][com[1]];
                                }
                            }

                            _helper.HelpChooseParameter(com.GetRange(0, 2));
                            return null;
                        }
                    case "help":
                        {
                            foreach(var p in _pool.Pool[com[0]].Keys)
                            {
                                if(p.Split(' ').Length == com.Count)
                                {
                                    return _pool.Pool[com[0]][p];
                                }
                            }

                            _helper.HelpChooseParameter(com.GetRange(0, 2));
                            return null;
                        }
                    case "refresh":
                        {
                            foreach (var p in _pool.Pool[com[0]].Keys)
                            {
                                if (p == com[1])
                                {
                                    return _pool.Pool[com[0]][com[1]];
                                }
                            }

                            _helper.HelpChooseParameter(com.GetRange(0, 2));
                            return null;
                        }
                    case "kill":
                        {
                            foreach (var p in _pool.Pool[com[0]].Keys)
                            {
                                if (p == com[1])
                                {
                                    return _pool.Pool[com[0]][com[1]];
                                }
                            }

                            _helper.HelpChooseParameter(com.GetRange(0, 2));
                            return null;
                        }
                    case "start":
                        {
                            foreach (var p in _pool.Pool[com[0]].Keys)
                            {
                                if (p == com[1])
                                {
                                    return _pool.Pool[com[0]][com[1]];
                                }
                            }

                            _helper.HelpChooseParameter(com.GetRange(0, 2));
                            return null;
                        }
                    default:
                        {
                            _helper.HelpChooseCommand(com[0]);
                            return null;
                        }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        } 
    }
}
