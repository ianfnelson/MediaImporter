using System;
using SystemWrapper.IO;

namespace MediaImporter.Framework.FileHandlers
{
    public class VideoFileHandler : IFileHandler
    {
        private readonly IConfigurationHelper _configurationHelper;
        private readonly INotifier _notifier;
        private readonly IFileWrap _fileWrap;
        private readonly IDirectoryWrap _directoryWrap;
        private readonly IPathWrap _pathWrap;

        public VideoFileHandler(IPathWrap pathWrap, IConfigurationHelper configurationHelper, INotifier notifier, IFileWrap fileWrap, IDirectoryWrap directoryWrap)
        {
            _pathWrap = pathWrap;
            _configurationHelper = configurationHelper;
            _notifier = notifier;
            _fileWrap = fileWrap;
            _directoryWrap = directoryWrap;
        }

        public short Ordinality
        {
            get { return 2; }
        }

        public bool CanHandleFile(string path)
        {
            return _pathWrap.HasExtension(path) &&
                   _configurationHelper.VideoExtensions.Contains(_pathWrap.GetExtension(path).ToLowerInvariant());
        }

        public void HandleFile(string path)
        {
            var videoYear = _fileWrap.GetLastWriteTime(path).DateTimeInstance.Year.ToString();

            var primaryOutputRoot = _configurationHelper.PrimaryVideoOutputLocation;
            var fileName = _pathWrap.GetFileName(path);

            var primaryOutputDir = _pathWrap.Combine(primaryOutputRoot, videoYear);
            var primaryOutputPath = _pathWrap.Combine(primaryOutputDir, fileName);

            if (_fileWrap.Exists(primaryOutputPath))
            {
                _notifier.Notify(string.Format("Skipping {0} - already exists", primaryOutputPath));
                return;
            }

            _notifier.Notify(string.Format("Copying {0} to {1}", fileName, primaryOutputPath));
            _directoryWrap.CreateDirectory(primaryOutputDir);
            _fileWrap.Copy(path, primaryOutputPath);

            foreach (var secondaryOutputRoot in _configurationHelper.SecondaryVideoOutputLocations)
            {
                var secondaryOutputDir = _pathWrap.Combine(secondaryOutputRoot, videoYear);
                var secondaryOutputPath = _pathWrap.Combine(secondaryOutputDir, fileName);
                _notifier.Notify(string.Format("Copying {0} to {1}", fileName, secondaryOutputPath));
                _directoryWrap.CreateDirectory(secondaryOutputDir);
                _fileWrap.Copy(path, secondaryOutputPath);
            }
        }
    }
}