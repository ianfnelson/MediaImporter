using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using SystemWrapper.IO;

namespace MediaImporter.Framework.FileHandlers
{
    public class ImageFileHandler : IFileHandler
    {
        private readonly IPathWrap _pathWrap;
        private readonly IConfigurationHelper _configurationHelper;
        private readonly INotifier _notifier;
        private readonly IFileWrap _fileWrap;
        private readonly IDirectoryWrap _directoryWrap;

        public ImageFileHandler(IPathWrap pathWrap, IConfigurationHelper configurationHelper, INotifier notifier, IFileWrap fileWrap, IDirectoryWrap directoryWrap)
        {
            _pathWrap = pathWrap;
            _configurationHelper = configurationHelper;
            _notifier = notifier;
            _fileWrap = fileWrap;
            _directoryWrap = directoryWrap;
        }

        public short Ordinality { get { return 1; } }

        public bool CanHandleFile(string path)
        {
            return _pathWrap.HasExtension(path) &&
                   _configurationHelper.ImageExtensions.Contains(_pathWrap.GetExtension(path).ToLowerInvariant());
        }

        public void HandleFile(string path)
        {
            var imageDate = GetPhotoDate(path);

            var primaryOutputRoot = _configurationHelper.PrimaryImageOutputLocation;

            var year = imageDate.Year.ToString();
            var month = imageDate.Month.ToString("00");
            var day = imageDate.Day.ToString("00");

            var relativePath = string.Format("{0}\\{0}-{1}\\{0}-{1}-{2}",
                                             year, month, day);

            var fileName = _pathWrap.GetFileName(path);
            var primaryOutputDir = _pathWrap.Combine(primaryOutputRoot, relativePath);
            var primaryOutputPath = _pathWrap.Combine(primaryOutputDir, fileName);

            if (_fileWrap.Exists(primaryOutputPath))
            {
                _notifier.Notify(string.Format("Skipping {0} - already exists", primaryOutputPath));
                return;
            }

            _notifier.Notify(string.Format("Copying {0} to {1}", fileName, primaryOutputPath));
            _directoryWrap.CreateDirectory(primaryOutputDir);
            _fileWrap.Copy(path, primaryOutputPath);
        
            foreach (var secondaryOutputRoot in _configurationHelper.SecondaryImageOutputLocations)
            {
                var secondaryOutputDir = _pathWrap.Combine(secondaryOutputRoot, relativePath);
                var secondaryOutputPath = _pathWrap.Combine(secondaryOutputDir, fileName);
                _notifier.Notify(string.Format("Copying {0} to {1}", fileName, secondaryOutputPath));
                _directoryWrap.CreateDirectory(secondaryOutputDir);
                _fileWrap.Copy(path, secondaryOutputPath);
            }
        }

        public DateTime GetPhotoDate(string path)
        {
            using (var image = Image.FromFile(path))
            {
                PropertyItem propertyItem;

                try
                {
                    propertyItem = image.GetPropertyItem(36867);
                }
                catch (ArgumentException)
                {
                    return _fileWrap.GetLastWriteTime(path).DateTimeInstance;
                }

                var sDate = Encoding.UTF8.GetString(propertyItem.Value).Trim();
                var secondHalf = sDate.Substring(sDate.IndexOf(" "), (sDate.Length - sDate.IndexOf(" ")));
                var firstHalf = sDate.Substring(0, 10);

                firstHalf = firstHalf.Replace(":", "-");
                sDate = firstHalf + secondHalf;

                return DateTime.Parse(sDate);
            }
        }
    }
}