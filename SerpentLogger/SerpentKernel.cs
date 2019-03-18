using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SerpentAPI.Common;
using SerpentAPI.Enums;
using SerpentAPI.Interfaces;

namespace SerpentLogger
{
    public class SerpentKernel
    {
        List<ISerpentEntry> RecordedEntries = new List<ISerpentEntry>();
        public EntrySeverity MinimumSeverity { get; set; }
        public EntrySeverity MaximumSeverity { get; set; }
        IRecordFlusher RecordFlusher { get; set; }

        public SerpentKernel()
        {
            MinimumSeverity = EntrySeverity.Informational;
            MaximumSeverity = EntrySeverity.Critical;
        }

        public void Record(ISerpentEntry entry)
        {
            SerpentValidation.ValidateSeverityRange(MinimumSeverity, MaximumSeverity);

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
