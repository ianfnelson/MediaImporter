using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaImporter.Framework
{
    public class ConfigurationHelper :  IConfigurationHelper
    {
        public IList<string> InputLocations { get; private set; }
        public IList<string> ImageExtensions { get; private set; }
        public IList<string> VideoExtensions { get; private set; }
        public IList<string> IgnoredExtensions { get; private set; }
        public string PrimaryImageOutputLocation { get; private set; }
        public IList<string> SecondaryImageOutputLocations { get; private set; }
        public string PrimaryVideoOutputLocation { get; private set; }
        public IList<string> SecondaryVideoOutputLocations { get; private set; }
        public string UnknownFileOutputLocation { get; private set; }
    }
}
