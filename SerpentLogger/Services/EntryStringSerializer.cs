using System.Collections.Generic;
using System.Text;
using SerpentAPI.Enums;
using SerpentAPI.Exceptions;
using SerpentAPI.Interfaces;

namespace SerpentLogger.Services
{
    public class EntryStringSerializer
    {
        public SerializerOptions Options;
        public string DateFormat { get; set; }

        public EntryStringSerializer()
        {
            Options = SerializerOptions.IncludeSeverity | SerializerOptions.IncludeMessage;
            DateFormat = "HH:mm:ss";
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
            if(Options == 0)
            {
                throw new InvalidSerializerOptionsException();
            }

            StringBuilder result = new StringBuilder();

            if(Options.HasFlag(SerializerOptions.IncludeDate))
            {
                result.Append($"[{entry.Date.ToString(this.DateFormat)}] ");
            }

            if(Options.HasFlag(SerializerOptions.IncludeSeverity))
            {
                result.Append($"[{entry.Severity.ToString()}] ");
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