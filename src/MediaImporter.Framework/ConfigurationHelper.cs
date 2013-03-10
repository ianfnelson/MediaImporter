using System.Collections.Generic;
using System.Configuration;

namespace MediaImporter.Framework
{
    public class ConfigurationHelper : IConfigurationHelper
    {
        private IList<string> _ignoredExtensions;
        private IList<string> _imageExtensions;
        private IList<string> _inputLocations;
        private IList<string> _secondaryImageOutputLocations;
        private IList<string> _secondaryVideoOutputLocations;
        private IList<string> _unknownFileOutputLocations;
        private IList<string> _videoExtensions;

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
                    _imageExtensions ??
                    (_imageExtensions = ConfigurationManager.AppSettings["ImageExtensions"].Split('|'));
            }
        }

        public IList<string> VideoExtensions
        {
            get
            {
                return
                    _videoExtensions ??
                    (_videoExtensions = ConfigurationManager.AppSettings["VideoExtensions"].Split('|'));
            }
        }

        public IList<string> IgnoredExtensions
        {
            get
            {
                return
                    _ignoredExtensions ??
                    (_ignoredExtensions = ConfigurationManager.AppSettings["IgnoredExtensions"].Split('|'));
            }
        }

        public string PrimaryImageOutputLocation
        {
            get { return ConfigurationManager.AppSettings["PrimaryImageOutputLocation"]; }
        }

        public IList<string> SecondaryImageOutputLocations
        {
            get
            {
                return
                    _secondaryImageOutputLocations ??
                    (_secondaryImageOutputLocations =
                     ConfigurationManager.AppSettings["SecondaryImageOutputLocations"].Split('|'));
            }
        }

        public string PrimaryVideoOutputLocation
        {
            get { return ConfigurationManager.AppSettings["PrimaryVideoOutputLocation"]; }
        }

        public IList<string> SecondaryVideoOutputLocations
        {
            get
            {
                return
                    _secondaryVideoOutputLocations ??
                    (_secondaryVideoOutputLocations =
                     ConfigurationManager.AppSettings["SecondaryVideoOutputLocations"].Split('|'));
            }
        }

        public IList<string> UnknownFileOutputLocations
        {
            get
            {
                return
                    _unknownFileOutputLocations ??
                    (_unknownFileOutputLocations =
                     ConfigurationManager.AppSettings["UnknownFileOutputLocations"].Split('|'));
            }
        }
    }
}