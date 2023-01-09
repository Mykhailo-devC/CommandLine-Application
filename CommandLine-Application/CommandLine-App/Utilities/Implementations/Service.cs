
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace CommandLine_App.GlobalCommands
{
    public class Service
    {
        public delegate void ServiceChangeEventHandler(object name, ServiceChangeEventArgs newStatus);

        public event ServiceChangeEventHandler OnChange;
        public ServiceController service;
        public ServiceControllerStatus status;

        public string Name { get; private set; }

        public Service(string name)
        {
            service = new ServiceController(name);
            status = service.Status;
            Name = service.ServiceName;
        }

        public void Refresh()
        {
            service.Refresh();

            if(service.Status != status)
            {
                OnChange.Invoke(this, new ServiceChangeEventArgs(
                                            Name: Name,
                                            OldStatus: status,
                                            NewStatus: service.Status
                                            ));
            }

            status = service.Status;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine(string.Concat(Enumerable.Repeat("-", service.DisplayName.Length + 11)));
            builder.AppendLine($"Name: {service.ServiceName},");
            builder.AppendLine($"Full name: {service.DisplayName}");
            builder.AppendLine($"Type: {service.ServiceType}");
            builder.AppendLine($"Can be paused: {service.CanPauseAndContinue}");
            builder.AppendLine($"Status: {service.Status}");
            builder.AppendLine(string.Concat(Enumerable.Repeat("-", service.DisplayName.Length + 11)));


            return builder.ToString();
        }
    }

}
