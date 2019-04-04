namespace SerpentKernel.Interfaces
{
    public interface IFileSystem
    {
        void AppendAllText(string path, string content);
        void WriteAllText(string path, string content);
    }
}