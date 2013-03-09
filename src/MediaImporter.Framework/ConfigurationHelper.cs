using System.Collections.Generic;
using System.Configuration;

namespace MediaImporter.Framework
{
    public class ConfigurationHelper : IConfigurationHelper
    {
        private IList<string> _inputLocations;
        private IList<string> _imageExtensions;
        private IList<string> _videoExtensions;
        private IList<string> _ignoredExtensions;

        public IList<string> InputLocations
        {
            get
            {
                return
                    _inputLocations ?? (_inputLocations = ConfigurationManager.AppSettings["InputLocations"].Split('|'));
            }
        }

        public IList<string> ImageExtensions
        {
            get
            {
                return
                    _imageExtensions ?? (_imageExtensions = ConfigurationManager.AppSettings["ImageExtensions"].Split('|'));
            }
        }

        public IList<string> VideoExtensions
        {
            get
            {
                return
                    _videoExtensions ?? (_videoExtensions = ConfigurationManager.AppSettings["VideoExtensions"].Split('|'));
            }
        }

        public IList<string> IgnoredExtensions
        {
            get
            {
                return
                    _ignoredExtensions ?? (_ignoredExtensions = ConfigurationManager.AppSettings["IgnoredExtensions"].Split('|'));
            }
        }

        public string PrimaryImageOutputLocation { get; private set; }

        public IList<string> SecondaryImageOutputLocations { get; private set; }

        public string PrimaryVideoOutputLocation { get; private set; }

        public IList<string> SecondaryVideoOutputLocations { get; private set; }

        public string UnknownFileOutputLocation { get; private set; }
    }
}