using CommandLine_App.GlobalCommands;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace CommandLine_App.Utilities.Implementations
{
    public static class ServiceRepository
    {
        public const string CONFIGURATION_FILE_NAME = "ObservedServices.xml";
        private const string SERVICE_ELEMENT = "service";
        private const string NAME_ATTRIBUTE = "name";

        private static readonly string _configFilePath;
        private static XDocument _config { get => XDocument.Load(_configFilePath); }


        static ServiceRepository()
        {
            _configFilePath = $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName}" +
                    @"\Configuration\" + CONFIGURATION_FILE_NAME;
        }
        private static XElement GetXElementByNameAttr(string name, XDocument config)
        {
            var element = config
                    .Root
                    .Elements(SERVICE_ELEMENT)
                    .FirstOrDefault(s => s.Attribute(NAME_ATTRIBUTE).Value == name);

            return element;
        }

        public static ServicePresentation[] GetAllServices()
        {
            Log.Information($"[Class:ServiceRepository][Method:{MethodBase.GetCurrentMethod().Name}] " +
                $"Getting all services from {CONFIGURATION_FILE_NAME} file.");

            var listOfServices = _config.Root.Elements(SERVICE_ELEMENT).Select(s =>
            {
                var service = new ServicePresentation(s.Attribute(NAME_ATTRIBUTE).Value);
                return service;
            });

            return listOfServices.ToArray();
        }

        public static ServicePresentation GetServiceByName(string serviceName)
        {
            Log.Information($"[Class:ServiceRepository][Method:{MethodBase.GetCurrentMethod().Name}] " +
                $"Getting {serviceName} service from {CONFIGURATION_FILE_NAME} file.");

            var XService = GetXElementByNameAttr(serviceName, _config);

            if (XService == null)
            {
                Log.Error($"[Class:ServiceRepository][Method:{MethodBase.GetCurrentMethod().Name}] " +
                    $"No service found with name '{serviceName}'");

                return null;
            }
                

            var service = new ServicePresentation(serviceName);

            return service;

        }

        public static void PrintAllServices()
        {
            Log.Information($"[Class:ServiceRepository][Method:{MethodBase.GetCurrentMethod().Name}] " +
                "Printing information about all services.");
            
            var services = GetAllServices();

            foreach (ServicePresentation service in services)
            {
                Console.WriteLine(service);
            }
        }

        public static void PrintServiceInfoByName(string name)
        {
            try
            {
                Log.Information($"[Class:ServiceRepository][Method:{MethodBase.GetCurrentMethod().Name}] " +
                $"Printing information about specificed service with name - '{name}'.");

                var service = GetServiceByName(name);

                if (service == null)
                {
                    Log.Warning($"[Class:ServiceRepository][Method:{MethodBase.GetCurrentMethod().Name}] " +
                        $"No existig service with {name} name.");
                    return;
                }

                Console.WriteLine(service);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[Class:ServiceRepository][Method:{MethodBase.GetCurrentMethod().Name}]" +
                    $" Getting service by name '{name}' failed.");
            }
        }

        public static void AddService(string name)
        {
            try
            {
                Log.Information($"[Class:ServiceRepository][Method:{MethodBase.GetCurrentMethod().Name}] " +
                $"Adding new service with name - '{name}'.");

                var doc = _config;

                doc.Root.Add(new XElement(SERVICE_ELEMENT,
                                new XAttribute(NAME_ATTRIBUTE, name)));

                Save(doc);

                Log.Information($"[Class:ServiceRepository][Method:{MethodBase.GetCurrentMethod().Name}] " +
                "Service has been added successfully.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[Class:ServiceRepository][Method:{MethodBase.GetCurrentMethod().Name}]" +
                    $" Adding new service '{name}' failed.");
            }
        }

        public static void RemoveService(string name)
        {
            try
            {
                Log.Information($"[Class:ServiceRepository][Method:{MethodBase.GetCurrentMethod().Name}] " +
                $"Removing service with name - '{name}'.");

                var doc = _config;

                var serviceToRemove = GetXElementByNameAttr(name, doc);

                if (serviceToRemove == null)
                {
                    Log.Warning($"[Class:ServiceRepository][Method:{MethodBase.GetCurrentMethod().Name}] " +
                        $"No existig service with {name} name.");
                    return;
                }

                serviceToRemove.Remove();
                Save(doc);

                Log.Information($"[Class:ServiceRepository][Method:{MethodBase.GetCurrentMethod().Name}] " +
                $"Service '{name}' has been removed successfully.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[Class:ServiceRepository][Method:{MethodBase.GetCurrentMethod().Name}]" +
                    " Removing service failed.");
            }
        }

        public static void UpdateService(string serviceName, string newName)
        {
            try
            {
                Log.Information($"[Class:ServiceRepository][Method:{MethodBase.GetCurrentMethod().Name}] " +
                $"Updating service with name '{serviceName}'.");

                var doc = _config;

                var serviceToUpdate = GetXElementByNameAttr(serviceName, doc);

                if (serviceToUpdate == null)
                {
                    Log.Warning($"[Class:ServiceRepository][Method:{MethodBase.GetCurrentMethod().Name}] " +
                        $"No existig service with {serviceName} name.");
                    return;
                }

                serviceToUpdate.Attribute(NAME_ATTRIBUTE).Value = newName;
                Save(doc);

                Log.Information($"[Class:ServiceRepository][Method:{MethodBase.GetCurrentMethod().Name}] " +
                $"Service has been updated with new name - '{newName}' successfully.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[Class:ServiceRepository][Method:{MethodBase.GetCurrentMethod().Name}]" +
                    $" Service '{serviceName}' updating failed");
            }
        }

        private static void Save(XDocument config)
        {
            config.Save(_configFilePath);
        }

    }
}
