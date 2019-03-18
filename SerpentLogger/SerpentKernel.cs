using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SerpentAPI.Common;
using SerpentAPI.Enums;
using SerpentAPI.Exceptions;
using SerpentAPI.Interfaces;

namespace SerpentLogger
{
    public class SerpentKernel
    {
        List<ISerpentEntry> RecordedEntries = new List<ISerpentEntry>();
        IRecordFlusher RecordFlusher { get; set; }

        public EntrySeverity MinimumSeverity { get; set; }
        public EntrySeverity MaximumSeverity { get; set; }
        public bool ForceFlushOnRecord = false;

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

                if(ForceFlushOnRecord)
                {
                    RecordFlusher.FlushSingleEntry(entry);
                }
            }
        }

        public IEnumerable<ISerpentEntry> GetRecords()
        {
            return RecordedEntries.AsReadOnly();
        }

        public void FlushRecords()
        {
            if(ForceFlushOnRecord)
            {
                throw new InvalidFlushOperationException();
            }
            
            RecordFlusher.SetRecordBuffer(RecordedEntries.AsReadOnly());
            RecordedEntries = new List<ISerpentEntry>();
            RecordFlusher.FlushRecordBuffer();
        }

        public void SetRecordFlusher(IRecordFlusher flusher)
        {
            RecordFlusher = flusher;
        }
    }
}
