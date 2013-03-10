using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SystemWrapper.IO;

namespace MediaImporter.Framework
{
    public class MediaProcessor : IMediaProcessor
    {
        private readonly IConfigurationHelper _configurationHelper;
        private readonly IDirectoryWrap _directoryWrap;
        private readonly INotifier _notifier;
        private readonly IFileHandler[] _fileHandlers;

        public MediaProcessor(IConfigurationHelper configurationHelper, IDirectoryWrap directoryWrap, INotifier notifier, IEnumerable<IFileHandler> fileHandlers )
        {
            _configurationHelper = configurationHelper;
            _directoryWrap = directoryWrap;
            _notifier = notifier;
            _fileHandlers = fileHandlers.OrderBy(x => x.Ordinality).ToArray();
        }

        public void ImportFiles()
        {
            var files = GetInputFiles();

            foreach (var file in files)
            {
                var handler = _fileHandlers.First(x => x.CanHandleFile(file));
                handler.HandleFile(file);
            }

            //Parallel.ForEach(files, file =>
            //    {
            //        var handler = _fileHandlers.First(x => x.CanHandleFile(file));
            //        handler.HandleFile(file);
            //    });
        }

        public virtual IEnumerable<string> GetInputFiles()
        {
            foreach (var inputLocation in _configurationHelper.InputLocations)
            {
                _notifier.Notify(inputLocation);

                if (!_directoryWrap.Exists(inputLocation)) continue;
                
                foreach (var file in _directoryWrap.GetFiles(inputLocation, "*.*", SearchOption.AllDirectories))
                {
                    yield return file;
                }
            }
        }
    }
}