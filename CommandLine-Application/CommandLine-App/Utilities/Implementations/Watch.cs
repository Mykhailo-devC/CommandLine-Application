using CommandLine_App.GlobalCommands;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace CommandLine_App.Utilities.Implementations
{
    public class Watch
    {
        private static Watch watch;
        public static Watch Instance
        {
            get
            {
                if (watch == null)
                {
                    watch = new Watch();
                }
                return watch;
            }
        }

        private bool IsWatching { get; set; }
        public ServicePresentation[] Services { get; private set; }
        private FileSystemWatcher _watcher;

        private readonly string configDirPath;
        private const int WATCH_REFRESH_TIME = 2000;

        private const NotifyFilters WATCHER_FILTERS = NotifyFilters.Attributes
                                     | NotifyFilters.CreationTime
                                     | NotifyFilters.DirectoryName
                                     | NotifyFilters.FileName
                                     | NotifyFilters.LastAccess
                                     | NotifyFilters.LastWrite
                                     | NotifyFilters.Security
                                     | NotifyFilters.Size;

        private Watch()
        {
            configDirPath = $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName}" +
                    @"\Configuration";
            Services = InitializeServices();
            IsWatching = false;
        }
        public void WatchStart()
        {
            Task.Run(() =>
            {
                try
                {
                    IsWatching = true;
                    _watcher = InitializeWatcher();

                    while (IsWatching)
                    {
                        if (_watcher == null || Services == null)
                            break;

                        foreach (ServicePresentation service in Services)
                        {
                            if (service == null)
                                break;

                            service.Refresh();
                            Console.WriteLine($"{service.Name} : {service.status}");
                        }

                        Thread.Sleep(WATCH_REFRESH_TIME);
                        Console.Clear();
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"[Class:{GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}]" +
                    $"Watch start error.");
                }
            });

        }

        public void WatchStop()
        {

            Log.Information($"[Class:{GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
            $"Watching has been stopped.");

            if (_watcher != null)
                _watcher.Dispose();

            IsWatching = false;
        }
        private FileSystemWatcher InitializeWatcher()
        {
            try
            {
                Log.Information($"[Class:{GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                "Starting watcher initializer.");

                var fileWatcher = new FileSystemWatcher(configDirPath);

                fileWatcher.NotifyFilter = WATCHER_FILTERS;

                fileWatcher.Changed += OnConfigChange;

                fileWatcher.EnableRaisingEvents = true;
                fileWatcher.Filter = ServiceRepository.CONFIGURATION_FILE_NAME;

                Log.Information($"[Class:{GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                "File watcher has been initialized successfully.");

                return fileWatcher;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[Class:{GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}]" +
                    " Initialize watcher error.");
                WatchStop();
                return null;
            }
        }

        private ServicePresentation[] InitializeServices()
        {
            try
            {
                Log.Information($"[Class:{GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                "Starting service initializer.");


                var services = ServiceRepository.GetAllServices().Select(s =>
                {
                    s.OnChange += OnStatusChange;
                    return s;
                });

                Log.Information($"[Class:{GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                "All services have been initialized successfully.");

                return services.ToArray();
            }

            catch (Exception ex)
            {
                Log.Error(ex, $"[Class:{GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}]" +
                    " Initialize service error.");
                WatchStop();
                return null;
            }
        }

        private void OnStatusChange(object sernder, ServiceChangeEventArgs e)
        {
            Log.Information($"[Class:{GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                $"{e.name} changed status from {e.oldStatus} to {e.newStatus}.");

            switch (e.newStatus)
            {
                case ServiceControllerStatus.Stopped:
                case ServiceControllerStatus.StopPending:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case ServiceControllerStatus.Running:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case ServiceControllerStatus.Paused:
                case ServiceControllerStatus.PausePending:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
            }

            Console.WriteLine($"{e.name} status has been changed to {e.newStatus}!");
            Console.ResetColor();
        }

        private void OnConfigChange(object sender, FileSystemEventArgs e)
        {
            Log.Information($"[Class:{GetType().Name}][Method:{MethodBase.GetCurrentMethod().Name}] " +
                $"{e.FullPath} configuration has been {e.ChangeType}");

            Services = InitializeServices();
            Console.WriteLine($"{e.Name} has been {e.ChangeType}!");

        }
    }

}
