using CommandLine_App.GlobalCommands;
using CommandLine_App.Utilities.Interfaces;
using Serilog;
using System;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Xml.Linq;

namespace CommandLine_App.Utilities.Implementations
{
    public class ServiceRepository : ServiceRepositoryAbstract
    {
        private XElement GetXElementByNameAttr(string name, XDocument config)
        {
            var element = config
                    .Root
                    .Elements(SERVICE_ELEMENT)
                    .FirstOrDefault(s => s.Attribute(NAME_ATTRIBUTE).Value == name);

            return element;
        }

        public override Service[] GetAllServices()
        {
            Log.Information($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                $"Getting all services from {CONFIGURATION_FILE_NAME} file.");

            var listOfServices = _config.Root.Elements(SERVICE_ELEMENT).Select(s =>
            {
                var service = new Service(s.Attribute(NAME_ATTRIBUTE).Value);
                return service;
            });

            return listOfServices.ToArray();
        }

        public override Service GetServiceByName(string serviceName)
        {
            Log.Information($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                $"Getting {serviceName} service from {CONFIGURATION_FILE_NAME} file.");

            var XService = GetXElementByNameAttr(serviceName, _config);

            if (XService == null)
            {
                Log.Error($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                    $"No service found with name '{serviceName}'");

                return null;
            }
                

            var service = new Service(serviceName);

            return service;

        }

        public override void PrintAllServices()
        {
            Log.Information($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                "Printing information about all services.");
            
            var services = GetAllServices();

            foreach (Service service in services)
            {
                Console.WriteLine(service);
            }
        }

        public override void PrintServiceInfoByName(string name)
        {
            try
            {
                Log.Information($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                $"Printing information about specificed service with name - '{name}'.");

                var service = GetServiceByName(name);

                if (service == null)
                {
                    Log.Warning($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                        $"No existig service with {name} name.");
                    return;
                }

                Console.WriteLine(service);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}]" +
                    $" Getting service by name '{name}' failed.");
            }
        }

        public override void AddService(string name)
        {
            try
            {
                Log.Information($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                $"Adding new service with name - '{name}'.");

                var doc = _config;

                doc.Root.Add(new XElement(SERVICE_ELEMENT,
                                new XAttribute(NAME_ATTRIBUTE, name)));

                Save(doc);

                Log.Information($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                "Service has been added successfully.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}]" +
                    $" Adding new service '{name}' failed.");
            }
        }

        public override void RemoveService(string name)
        {
            try
            {
                Log.Information($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                $"Removing service with name - '{name}'.");

                var doc = _config;

                var serviceToRemove = GetXElementByNameAttr(name, doc);

                if (serviceToRemove == null)
                {
                    Log.Warning($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                        $"No existig service with {name} name.");
                    return;
                }

                serviceToRemove.Remove();
                Save(doc);

                Log.Information($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                $"Service '{name}' has been removed successfully.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}]" +
                    " Removing service failed.");
            }
        }

        public override void UpdateService(string serviceName, string newName)
        {
            try
            {
                Log.Information($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                $"Updating service with name '{serviceName}'.");

                var doc = _config;

                var serviceToUpdate = GetXElementByNameAttr(serviceName, doc);

                if (serviceToUpdate == null)
                {
                    Log.Warning($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                        $"No existig service with {serviceName} name.");
                    return;
                }

                serviceToUpdate.Attribute(NAME_ATTRIBUTE).Value = newName;
                Save(doc);

                Log.Information($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                $"Service has been updated with new name - '{newName}' successfully.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}]" +
                    $" Service '{serviceName}' updating failed");
            }
        }

    }
}
