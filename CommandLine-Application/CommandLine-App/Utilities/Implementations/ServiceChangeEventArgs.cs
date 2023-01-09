using System.Data;
using System.Reflection.Emit;
using System.ServiceProcess;

namespace CommandLine_App.GlobalCommands
{
    public class ServiceChangeEventArgs
    {
        public string name;
        public ServiceControllerStatus oldStatus;
        public ServiceControllerStatus newStatus;

        public ServiceChangeEventArgs(
            string Name,
            ServiceControllerStatus OldStatus,
            ServiceControllerStatus NewStatus)
        {
            name = Name;
            oldStatus = OldStatus;
            newStatus = NewStatus;
        }
    }
}