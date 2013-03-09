using System.IO;
using SystemWrapper.IO;

namespace MediaImporter.Framework.FileHandlers
{
    public class UnknownFileHandler : IFileHandler
    {
        private readonly IConfigurationHelper _configurationHelper;
        private readonly INotifier _notifier;
        private readonly IFileWrap _fileWrap;
        private readonly IPathWrap _pathWrap;

        public UnknownFileHandler(IConfigurationHelper configurationHelper, INotifier notifier, IFileWrap fileWrap, IPathWrap pathWrap)
        {
            _configurationHelper = configurationHelper;
            _notifier = notifier;
            _fileWrap = fileWrap;
            _pathWrap = pathWrap;
        }

        public short Ordinality { get { return short.MaxValue; } }

        public bool CanHandleFile(string path)
        {
            return true;
        }

        public void HandleFile(string path)
        {
            _notifier.Notify("Unknown - " + path);

            var fileName = _pathWrap.GetFileName(path);

            foreach (var outputLocation in _configurationHelper.UnknownFileOutputLocations)
            {
                var outputPath = _pathWrap.Combine(outputLocation, fileName);

                if (!_fileWrap.Exists(outputPath))
                {
                    _fileWrap.Copy(path, outputPath);
                }
            }
        }
    }
}