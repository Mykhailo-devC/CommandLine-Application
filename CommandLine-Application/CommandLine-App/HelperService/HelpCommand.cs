using CommandLine_App.Abstraction;
using CommandLine_App.Pools;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CommandLine_App.HelperService
{
    public class HelpCommand
    {
        private readonly IHelper _helper;
        public HelpCommand(Helper helper)
        {
            _helper = helper;
        }
        public void Help(List<string> userInput)
        {
            try
            {
                switch (userInput.Count)
                {
                    case 1: GlobalHelp(); break;
                    case 2: CommandHelp(userInput[1]); break;
                    case 3: ParamHelp(userInput[1], userInput[2]); break;
                    default:
                        Log.Warning("[Class:{0}][Method:{1}] User inputs invalid command! [Input = {2}]", this.GetType(), MethodBase.GetCurrentMethod().Name, userInput);
                        break;
                }
            }
            catch(Exception ex)
            {
                Log.Error(ex, "[Class:{0}][Method:{1}][Input = {2}]", this.GetType(), MethodBase.GetCurrentMethod().Name, userInput);
            }
        }

        private void ParamHelp(string command, string child)
        {
            var definedCommandType = typeof(Command)
                .Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Command)))
                .FirstOrDefault(t => t.Name.ToLower() == command);

            if(definedCommandType == null)
            {
                Log.Warning($"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}] User inputs invalid command!, [input = '{command} {child}']");
                _helper.HelpChooseCommand(command);
                return;
            }

            var definedChildType = definedCommandType
                .Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(definedCommandType))
                .FirstOrDefault(t => t.Name.ToLower() == command + child);

            if(definedChildType == null)
            {
                Log.Warning($"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}] User inputs invalid command!, [input = '{command} {child}']");
                _helper.HelpChooseParameter(child);
                return;
            }

            var definedChild = Activator.CreateInstance(definedChildType);

            Console.Write(definedChildType.GetProperty("ArgumentDescription").GetValue(definedChild));
            Console.Write(definedChildType.GetMethod("ToString").Invoke(definedChild, null));
        }

        private void CommandHelp(string command)
        {
            var definedCommandType = typeof(Command)
                .Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Command)))
                .FirstOrDefault(t => t.Name.ToLower() == command);

            if (definedCommandType == null)
            {
                Log.Warning($"[Class:{this.GetType()}][Method:{MethodBase.GetCurrentMethod().Name}] User inputs invalid command!, [input = '{command}']");
                _helper.HelpChooseCommand(command);
                return;
            }

            var children = definedCommandType
                .Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(definedCommandType))
                .Select(t => Activator.CreateInstance(t));

            var child = children.FirstOrDefault();

            Console.WriteLine(child.GetType().BaseType.GetMethod("ToString").Invoke(child, null));
            foreach (var ch in children)
            {
                Console.WriteLine(ch.GetType().GetMethod("ToString").Invoke(ch, null));
            }

        }

        private void GlobalHelp()
        {
            var commands = typeof(Command)
                .Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Command)) && t.IsAbstract)
                .Select(t => t);

            foreach (var c in commands)
            {
                var children = c
                    .Assembly.GetTypes()
                    .Where(t => t.IsSubclassOf(c))
                    .Select(t => Activator.CreateInstance(t));

                var child = children.FirstOrDefault();

                Console.WriteLine(child.GetType().BaseType.GetMethod("ToString").Invoke(child, null));
                foreach (var ch in children)
                {
                    Console.WriteLine(ch.GetType().GetMethod("ToString").Invoke(ch, null));
                }
            }
        }
    }
}
