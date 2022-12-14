using CommandLine_App.Commands;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CommandLine_App.GlobalCommands.StartCommandChildren
{
    /*public class StartFileCommand : StartCommand
    {
        public StartFileCommand()
        {
            Description = "start file";
            ArgumentDescription = "Start file (string value)," +
                "like [start file C:\\Program Files\\Mozilla Firefox\\firefox.exe].\n";
        }
        public override bool Execute(params string[] param)
        {
            try
            {
                if(param.Any())
                    return StartFile(ConcatFilePath(param));

                Log.Warning("[{1}] User inputs incorrect count of parameters, [params = '{0}']", param, this.GetType());
                PrintArgumentTip();
                return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[{1}] Exeption has been thrown from Execute! [params = '{0}']", param,this.GetType());
                return false;
            }
        }
        public override string ToString()
        {
            return $"\t'{Description}' [path_value] - starts the process with specified file path.";
        }

        private bool StartFile(string path)
        {
            try
            {
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = path;
                Process.Start(info);

                Log.Information("[{0}] Execute has been finished successfully!", this.GetType());
                Console.WriteLine($"Proccess at {path} was started!");
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string ConcatFilePath(string[] param)
        {
            var reg = new Regex(@"^.:\w*");

            if (reg.IsMatch(param[0]))
            {
                var str = new StringBuilder();

                foreach (var item in param)
                {
                    str.Append(item);
                    str.Append(' ');
                }

                return str.ToString();
            }

            
            throw new Exception("File path isn't valid");
        }
    }*/
}
