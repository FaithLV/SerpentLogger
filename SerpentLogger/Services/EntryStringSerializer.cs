using System.Collections.Generic;
using System.Text;
using SerpentAPI.Enums;
using SerpentAPI.Interfaces;

namespace SerpentLogger.Services
{
    public class EntryStringSerializer
    {
        public SerializerOptions Options;

        public EntryStringSerializer()
        {
            Options = SerializerOptions.IncludeSeverity | SerializerOptions.IncludeMessage;
        }

        public ICollection<string> Serialize(IEnumerable<ISerpentEntry> entries)
        {
            var collection = new List<string>();

            foreach (var entry in entries)
            {
                var serialized = Serialize(entry);
                collection.Add(serialized);
            }

            return collection;
        }

        public string Serialize(ISerpentEntry entry)
        {
            StringBuilder result = new StringBuilder();

            if(Options.HasFlag(SerializerOptions.IncludeDate))
            {
                result.Append($"[{entry.Date.Minute}:{entry.Date.Second}] ");
            }

            if(Options.HasFlag(SerializerOptions.IncludeSeverity))
            {
                result.Append($"[{nameof(entry.Severity)}] ");
            }

            if(Options.HasFlag(SerializerOptions.IncludeOrigin))
            {
                result.Append($"[{entry.Origin}] : ");
            }

            if(Options.HasFlag(SerializerOptions.IncludeMessage))
            {
                result.Append(entry.Message);
            }

            return result.ToString();
        }
    }
}