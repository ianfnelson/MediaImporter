using System;
using SystemWrapper.IO;

namespace MediaImporter.Framework.FileHandlers
{
    public class ImageFileHandler : IFileHandler
    {
        private readonly IPathWrap _pathWrap;
        private readonly IConfigurationHelper _configurationHelper;
        private readonly INotifier _notifier;
        private readonly IFileWrap _fileWrap;

        public ImageFileHandler(IPathWrap pathWrap, IConfigurationHelper configurationHelper, INotifier notifier, IFileWrap fileWrap)
        {
            _pathWrap = pathWrap;
            _configurationHelper = configurationHelper;
            _notifier = notifier;
            _fileWrap = fileWrap;
        }

        public short Ordinality { get { return 1; } }

        public bool CanHandleFile(string path)
        {
            return _pathWrap.HasExtension(path) &&
                   _configurationHelper.ImageExtensions.Contains(_pathWrap.GetExtension(path).ToLowerInvariant());
        }

        public void HandleFile(string path)
        {
            _notifier.Notify("Image - " + path);

            // Build destination folder structure

            // check if primary output exists

            // Copy to primary output

            // Copy to secondary outputs
        }
    }
}