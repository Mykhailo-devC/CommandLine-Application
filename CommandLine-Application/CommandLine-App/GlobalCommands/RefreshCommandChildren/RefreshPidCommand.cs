using CommandLine_App.Abstraction;
using CommandLine_App.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CommandLine_App.GlobalCommands.RefreshCommandChildren
{
    public class RefreshPidCommand : RefreshCommand
    {
        public new string Name { get; set; }
        public override string ArgumentDescription { get; set; }
        public RefreshPidCommand()
        {
            Name = "refresh pid";
            ArgumentDescription = "Refresh pid (string value)," +
                "like [refresh name 242].\n";
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

                return RefreshByPID(int.Parse(param.First()));
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
            return $"\t'{Name}' [pid_value] - refresh running process" +
                $" with specified process identifier";
        }

        private bool RefreshByPID(int arg)
        {
            try
            {
                var process = Process.GetProcessById(arg);

                process.Refresh();

                Console.WriteLine($"{process.ProcessName} was refreshed");

                return true;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("No existing processes with [{0}] id!", arg);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
