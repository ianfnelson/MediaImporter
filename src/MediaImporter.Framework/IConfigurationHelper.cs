using System.Collections.Generic;

namespace MediaImporter.Framework
{
    public interface IConfigurationHelper
    {
        IList<string> InputLocations { get; }

        IList<string> ImageExtensions { get; }

        IList<string> VideoExtensions { get; }

        IList<string> IgnoredExtensions { get; }

        string PrimaryImageOutputLocation { get; }

        IList<string> SecondaryImageOutputLocations { get; } 

        string PrimaryVideoOutputLocation { get; }

        IList<string> SecondaryVideoOutputLocations { get; }

        IList<string> UnknownFileOutputLocations { get; }
    }
}