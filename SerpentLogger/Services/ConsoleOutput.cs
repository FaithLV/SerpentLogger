using SerpentAPI.Interfaces;

namespace SerpentLogger.Services
{
    public class ConsoleOutput : IDirectOutput
    {
        private readonly EntryStringSerializer _serializer;

        public ConsoleOutput(EntryStringSerializer serializer)
        {
            _serializer = serializer;
        }

        public void WriteEntryLine(ISerpentEntry entry)
        {
            var serialized = _serializer.Serialize(entry);
            System.Console.WriteLine(serialized);
        }
    }
}