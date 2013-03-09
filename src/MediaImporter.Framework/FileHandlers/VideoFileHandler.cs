using System;
using SystemWrapper.IO;

namespace MediaImporter.Framework.FileHandlers
{
    public class VideoFileHandler : IFileHandler
    {
        private readonly IConfigurationHelper _configurationHelper;
        private readonly INotifier _notifier;
        private readonly IPathWrap _pathWrap;

        public VideoFileHandler(IPathWrap pathWrap, IConfigurationHelper configurationHelper, INotifier notifier)
        {
            _pathWrap = pathWrap;
            _configurationHelper = configurationHelper;
            _notifier = notifier;
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
            _notifier.Notify("Video - " + path);
        }
    }
}