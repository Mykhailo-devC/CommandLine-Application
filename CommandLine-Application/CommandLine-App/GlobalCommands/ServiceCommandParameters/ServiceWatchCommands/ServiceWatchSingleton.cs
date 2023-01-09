using CommandLine_App.Utilities.Implementations;

namespace CommandLine_App.GlobalCommands.ServiceCommandParameters.ServiceWatchCommands
{
    public static class ServiceWatchSingleton
    {
        private static Watch _watch;
        static ServiceWatchSingleton() { }

        public static Watch Instance
        {
            get
            {
                if (_watch == null)
                {
                    _watch = new Watch(ServiceWatch._repository);
                }
                return _watch;
            }
        }

    }
}
