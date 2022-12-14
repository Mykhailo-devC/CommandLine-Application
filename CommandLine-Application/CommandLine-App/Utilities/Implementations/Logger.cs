using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommandLine_App.Logging
{
    public static class Logger
    {
        public static readonly string LOGGING_PATH;
        static Logger()
        {
            LOGGING_PATH = $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName}\\Logging\\log.txt";
        }

        public static void LoggerSetup()
        {
            var flow = Guid.NewGuid();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(LOGGING_PATH,
                    outputTemplate: $"|Flow:{flow}| " + "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
