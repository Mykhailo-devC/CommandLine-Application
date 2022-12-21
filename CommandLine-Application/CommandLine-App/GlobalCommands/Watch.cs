using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CommandLine_App.GlobalCommands
{

    public class Watch
    {
        public bool IsWatching { get; set; }
        public Service[] Services { get; private set; }
        private FileSystemWatcher watcher;
        private readonly string ConfigPath;

        public Watch()
        {
            ConfigPath = $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName}" +
                    $"\\Configuration";

            Services = InitializeServices();
            IsWatching = false;
        }
        public async void WatchStart()
        {
            await Task.Run(() => 
            {
                try
                {
                    IsWatching = true;
                    watcher = InitializeWatcher();

                    while (IsWatching)
                    {
                        if (watcher == null || Services == null)
                            break;

                        foreach (Service service in Services)
                        {
                            if (service == null)
                                break;

                            service.Refresh();
                            Console.WriteLine($"{service.Name} : {service.status}");
                        }

                        Thread.Sleep(3000);
                        Console.Clear();
                    }
                }
                catch(Exception ex)
                {
                    Log.Error(ex, $"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}]" +
                    $"Watch start error.");
                }
            });
            
        }

        public void WatchStop()
        {
            watcher.Dispose();
            IsWatching = false;
        }
        #region CRUD operations
        public void GetAllServices()
        {
            Log.Information($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                $"Printing information about all services.");

            foreach (Service service in Services)
            {
                Console.WriteLine(service);
            }
        }
        public void AddService(string name)
        {
            try
            {
                Log.Information($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                $"Adding new service.");

                XDocument xdoc = XDocument.Load
                (ConfigPath + "\\ObservedServices.xml");
                var services = xdoc.Root;

                services.Add(new XElement("service",
                                new XAttribute("name", name))
                            );
                xdoc.Save(ConfigPath + "\\ObservedServices.xml");
            }
            catch(Exception ex)
            {
                Log.Error(ex, $"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}]" +
                    $" Adding new service failed.");
            }
        }

        public void RemoveService(string name)
        {
            try
            {
                Log.Information($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                $"Removing new service.");

                XDocument xdoc = XDocument.Load
                (ConfigPath + "\\ObservedServices.xml");
                var services = xdoc.Root.Elements("service");

                var serviceToRemove = services.FirstOrDefault(s => s.Attribute("name").Value == name);

                if(serviceToRemove == null)
                {
                    Log.Warning($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                        $"No existig service with {name} name.");
                }

                serviceToRemove.Remove();
                xdoc.Save(ConfigPath + "\\ObservedServices.xml");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}]" +
                    $" Removing service failed.");
            }
        }

        public void UpdateService(string serviceName, string newName)
        {
            try
            {
                Log.Information($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                $"Updating new service.");

                XDocument xdoc = XDocument.Load
                (ConfigPath + "\\ObservedServices.xml");
                var services = xdoc.Root.Elements("service");

                var serviceToUpdate = services.FirstOrDefault(s => s.Attribute("name").Value == serviceName);

                if (serviceToUpdate == null)
                {
                    Log.Warning($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                        $"No existig service with {serviceName} name.");
                }

                serviceToUpdate.Attribute("name").Value = newName;
                xdoc.Save(ConfigPath + "\\ObservedServices.xml");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}]" +
                    $" Service updating failed");
            }
        }

        public void GetServiceByName(string name)
        {
            try
            {
                Log.Information($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                $"Printing information about specificed service.");

                var service = Services.FirstOrDefault(s => s.Name == name);

                if (service == null)
                {
                    Log.Warning($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                        $"No existig service with {name} name.");
                }

                Console.WriteLine(service);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}]" +
                    $" Getting service by name failed.");
            }
        }
        #endregion
        private FileSystemWatcher InitializeWatcher()
        {
            try
            {
                Log.Information($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                $"Starting watcher initializer.");

                var fileWatcher = new FileSystemWatcher(ConfigPath);

                fileWatcher.NotifyFilter = NotifyFilters.Attributes
                                     | NotifyFilters.CreationTime
                                     | NotifyFilters.DirectoryName
                                     | NotifyFilters.FileName
                                     | NotifyFilters.LastAccess
                                     | NotifyFilters.LastWrite
                                     | NotifyFilters.Security
                                     | NotifyFilters.Size;

                fileWatcher.Changed += OnConfigChange;

                fileWatcher.EnableRaisingEvents = true;
                fileWatcher.Filter = "ObservedServices.xml";

                return fileWatcher;
            }
            catch(Exception ex)
            {
                Log.Error(ex, $"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}]" +
                    $" Initialize watcher error.");

                return null;
            }
        }

        private Service[] InitializeServices()
        {
            try
            {
                Log.Information($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                $"Starting serice initializer.");

                XDocument xdoc = XDocument.Load
                (ConfigPath + "\\ObservedServices.xml");
                var services = xdoc.Root.Elements("service");

                var listOfServices = new Service[services.Count()];

                try
                {
                    for (int i = 0; i < services.Count(); i++)
                    {
                        var service = new Service(services.ElementAtOrDefault(i).Attribute("name").Value);
                        service.OnChange += OnStatusChange;
                        listOfServices[i] = service;
                    }
                }
                catch (InvalidOperationException ex)
                {
                    Log.Error(ex, $"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}]" +
                        $" Initialize service error.");
                }

                return listOfServices;
            }
            
            catch(Exception ex)
            {
                Log.Error(ex, $"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}]" +
                    $" Initialize service error.");

                return null;
            }
        }

        private void OnStatusChange(string serviceName, ServiceControllerStatus newStatus)
        {
            Log.Information($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                $"{serviceName} changed status to {newStatus}.");

            if (newStatus == ServiceControllerStatus.Stopped ||
                    newStatus == ServiceControllerStatus.StopPending)
                Console.ForegroundColor = ConsoleColor.Red;

            if (newStatus == ServiceControllerStatus.Paused ||
                newStatus == ServiceControllerStatus.PausePending)
                Console.ForegroundColor = ConsoleColor.Yellow;

            if (newStatus == ServiceControllerStatus.Running)
                Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine($"{serviceName} status has been changed to {newStatus}!");
            Console.ResetColor();
        }

        private void OnConfigChange(object sender, FileSystemEventArgs e)
        {
            Log.Information($"[Class:{this.GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                $"{e.FullPath} configuration has been {e.ChangeType}");

            var changeingTask = Task.Run(() =>
            {
                Services = InitializeServices();
                Console.WriteLine($"{e.Name} has been {e.ChangeType}!");
            });

            changeingTask.Wait();
        }
    }

}
