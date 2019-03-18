using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SerpentAPI.Enums;
using SerpentAPI.Interfaces;

namespace SerpentLogger
{
    public class SerpentKernel
    {
        static List<ISerpentEntry> RecordedEntries = new List<ISerpentEntry>();
        EntrySeverity MinimumSeverity { get; set; }
        EntrySeverity MaximumSeverity { get; set; }
        IRecordFlusher RecordFlusher { get; set; }

        public SerpentKernel()
        {
            MinimumSeverity = EntrySeverity.Low;
            MaximumSeverity = EntrySeverity.Critical;
        }

        public void Record(ISerpentEntry entry)
        {
            if(entry.Severity >= MinimumSeverity && entry.Severity <= MaximumSeverity)
            {
                RecordedEntries.Add(entry);
            }
        }

        public IEnumerable<ISerpentEntry> GetRecords()
        {
            return RecordedEntries.AsReadOnly();
        }

        public void FlushRecords()
        {
            RecordFlusher.SetRecordBuffer(RecordedEntries.AsReadOnly());
            RecordedEntries = new List<ISerpentEntry>();
            RecordFlusher.FlushRecordBuffer();
        }
    }
}
