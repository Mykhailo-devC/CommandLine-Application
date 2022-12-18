using System.Diagnostics;

namespace CommandLine_App.Utilities.Implementations
{
    public class ProcessWrapper
    {
        public virtual Process[] GetProcesses()
        {
            return Process.GetProcesses();
        }

        public Process[] GetProcessesByName(string name)
        {
            return Process.GetProcessesByName(name);
        }
        public Process GetProcessById(int id)
        {
            return Process.GetProcessById(id);
        }

        public bool Start(string name)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = name;
            Process.Start(info);

            return true;
        }

        public void Kill(Process process)
        {
            process.Kill();
        }

        public void Refresh(Process process)
        {
            process.Refresh();
        }
    }
}
