using CommandLine_App.Abstraction;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CommandLine_App.Commands
{
    public abstract class Show : Command
    {
        public override abstract bool Execute(params string[] param);

        protected string ProcessesToString(IEnumerable<Process> processes)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(new string('-', 54));
            sb.Append(string.Format("\n|{0,-5}|{1,-36}|{2, 6}/Kb|\n", "Id", "Process Description", "Memory"));
            sb.Append(new string('-', 54));

            foreach (var p in processes)
            {
                sb.Append(string.Format("\n|{0,-5}|{1,-36}|{2, 6}/Kb|", p.Id, p.ProcessName, p.PrivateMemorySize64 / 1024));
            }

            return sb.ToString();
        }
    }
}
