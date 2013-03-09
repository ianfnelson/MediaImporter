namespace MediaImporter.Framework.FileHandlers
{
    public class UnknownFileHandler : IFileHandler
    {
        private readonly IConfigurationHelper _configurationHelper;
        private readonly INotifier _notifier;

        public UnknownFileHandler(IConfigurationHelper configurationHelper, INotifier notifier)
        {
            _configurationHelper = configurationHelper;
            _notifier = notifier;
        }

        public short Ordinality { get { return short.MaxValue; } }

        public bool CanHandleFile(string path)
        {
            return true;
        }

        public void HandleFile(string path)
        {
            _notifier.Notify("Unkno - " + path);
        }
    }
}