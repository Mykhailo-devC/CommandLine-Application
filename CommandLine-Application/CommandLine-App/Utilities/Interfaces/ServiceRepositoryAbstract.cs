using CommandLine_App.GlobalCommands;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace CommandLine_App.Utilities.Interfaces
{
    public abstract class ServiceRepositoryAbstract
    {
        protected const string CONFIGURATION_FILE_NAME = "ObservedServices.xml";
        protected const string SERVICE_ELEMENT = "service";
        protected const string NAME_ATTRIBUTE = "name";

        protected readonly string _configFilePath;
        protected XDocument _config { get => XDocument.Load(_configFilePath); }
        

        public ServiceRepositoryAbstract()
        {
            _configFilePath = $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName}" +
                    @"\Configuration\" + CONFIGURATION_FILE_NAME;
        }
        public abstract Service[] GetAllServices();
        public abstract Service GetServiceByName(string serviceName);
        public abstract void AddService(string name);
        public abstract void RemoveService(string name);
        public abstract void UpdateService(string serviceName, string newName);
        public abstract void PrintServiceInfoByName(string name);
        public abstract void PrintAllServices();

        protected void Save(XDocument config)
        {
            config.Save(_configFilePath);
        }
        
        
    }
}
