using CommandLine_App.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CommandLine_App.GlobalCommands.RefreshCommandChildren
{
    public class RefreshNameCommand : RefreshCommand
    {
        public new string Name { get; set; }
        public override string ArgumentDescription { get; set; }
        public RefreshNameCommand()
        {
            Name = "refresh name";
            ArgumentDescription = "Refresh name (string value)," +
                "like [refresh name firefox].\n";
        }
        public override bool Execute(params string[] param)
        {
            try
            {
                if (param.Length != 1)
                {
                    PrintArgumentTip();
                    return false;
                }

                return RefreshByName(param.First());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public override void PrintBaseToString()
        {
            Console.WriteLine(base.ToString());
        }

        public override string ToString()
        {
            return $"\t'{Name}' [name_value] - refresh the running process with specified name.";
        }
        private bool RefreshByName(string arg)
        {
            try
            {
                var processes = Process.GetProcessesByName(arg).OrderBy(e => e.Id);

                if (processes.Count() == 0)
                {
                    Console.WriteLine("No existing processes with '{0}' name!", arg);
                }
                else
                {
                    Console.WriteLine($"{processes.First().ProcessName} was refreshed!");
                    foreach(var process in processes)
                    {
                        process.Refresh();
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
