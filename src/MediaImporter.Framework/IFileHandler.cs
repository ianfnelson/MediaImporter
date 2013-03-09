namespace MediaImporter.Framework
{
    public interface IFileHandler
    {
        short Ordinality { get; }

        bool CanHandleFile(string path);

        void HandleFile(string path);
    }
}