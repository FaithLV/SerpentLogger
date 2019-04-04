using System.IO;
using SerpentKernel.Interfaces;

namespace SerpentKernel.Abstractions
{
    public class FileSystem : IFileSystem
    {
        public void AppendAllText(string path, string content)
        {
            File.AppendAllText(path, content);
        }

        public void WriteAllText(string path, string content)
        {
            File.WriteAllText(path, content);
        }
    }
}