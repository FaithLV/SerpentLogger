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
        IRecordFlusher _recordFlusher { get; set; }
        IDirectOutput _directOutput { get; set; }

        public EntrySeverity MinimumSeverity { get; set; }
        public EntrySeverity MaximumSeverity { get; set; }
        public bool ForceFlushOnRecord = false;
        public bool SendRecordsToOutput = true;

        public SerpentKernel(IRecordFlusher recordFlusher, IDirectOutput directOutput)
        {
            _recordFlusher = recordFlusher;
            _directOutput = directOutput;

            MinimumSeverity = EntrySeverity.Informational;
            MaximumSeverity = EntrySeverity.Critical;
        }

        public void Record(ISerpentEntry entry)
        {
            SerpentValidation.ValidateSeverityRange(MinimumSeverity, MaximumSeverity);

            if(entry.Severity >= MinimumSeverity && entry.Severity <= MaximumSeverity)
            {
                RecordedEntries.Add(entry);
                WriteRecordToOutput(entry);

                if(ForceFlushOnRecord)
                {
                    _recordFlusher.FlushSingleEntry(entry);
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
            
            _recordFlusher.SetRecordBuffer(RecordedEntries.AsReadOnly());
            RecordedEntries = new List<ISerpentEntry>();
            _recordFlusher.FlushRecordBuffer();
        }

        public void SetRecordFlusher(IRecordFlusher flusher)
        {
            _recordFlusher = flusher;
        }

        public void SetDirectOutput(IDirectOutput output)
        {
            _directOutput = output;
        }

        private void WriteRecordToOutput(ISerpentEntry entry)
        {
            if(SendRecordsToOutput)
            {
                _directOutput.WriteEntryLine(entry);
            }
        }
    }
}
