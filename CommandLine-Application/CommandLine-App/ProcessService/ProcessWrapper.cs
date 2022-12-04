using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CommandLine_App.ProcessService
{
    public class ProcessWrapper : Process
    {
        public new Process[] GetProcesses()
        {
            return GetProcesses();
        }

        public new Process[] GetProcessesByName(string name)
        {
            return GetProcessesByName(name);
        }
        public new Process GetProcessById(int id)
        {
            return Process.GetProcessById(id);
        }

        public new bool Start(string name)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = name;
            Start(info);

            return true;
        }

        public new void Kill(Process process)
        {
            process.Kill();
        }

        public new void Refresh(Process process)
        {
            process.Refresh();
        }
    }
}
