﻿using CommandLine_App.Commands;
using Serilog;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace CommandLine_App.GlobalCommands.RefreshCommandChildren
{
    public class RefreshName : Refresh
    {
        public override bool Execute(params string[] param)
        {
            try
            {
                RefreshByName(param.First());
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[Class:{0}][Method:{1}][Parameters = {2}]", this.GetType().Name, MethodBase.GetCurrentMethod().Name, param);
                return false;
            }
        }

        private void RefreshByName(string arg)
        {
            var processes = Process.GetProcessesByName(arg).OrderBy(e => e.Id);

            if (processes.Count() == 0)
            {
                Log.Warning($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] No existing processes with {arg} name!");
                Console.WriteLine("No existing processes with '{0}' name!", arg);
            }
            else
            {
                Log.Information($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] finished successfully!");
                foreach (var process in processes)
                {
                    process.Refresh();
                }
            }
        }
    }
}
