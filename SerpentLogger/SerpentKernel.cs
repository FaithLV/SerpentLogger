using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SerpentAPI.Common;
using SerpentAPI.Enums;
using SerpentAPI.Exceptions;
using SerpentAPI.Interfaces;

namespace SerpentLogger
{
    public class Kernel
    {
        List<ISerpentEntry> RecordedEntries = new List<ISerpentEntry>();
        IRecordFlusher _recordFlusher { get; set; }
        IDirectOutput _directOutput { get; set; }

        public EntrySeverity MinimumSeverity { get; set; }
        public EntrySeverity MaximumSeverity { get; set; }
        public bool ForceFlushOnRecord = false;
        public bool SendRecordsToOutput = true;

        public Kernel(IRecordFlusher recordFlusher, IDirectOutput directOutput)
        {
            _recordFlusher = recordFlusher;
            _directOutput = directOutput;

            MinimumSeverity = EntrySeverity.Informational;
            MaximumSeverity = EntrySeverity.Critical;
        }

        /// <summary> Write an entry to log record buffer
        /// <param name="entry">Entry to write</param>
        /// </summary>
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

        /// <summary> Get all records from currenty record buffer
        /// </summary>
        public IEnumerable<ISerpentEntry> GetRecords()
        {
            return RecordedEntries.AsReadOnly();
        }


        /// <summary> Clear current log buffers and flush to current IFlusher
        /// </summary>
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

        private void WriteRecordToOutput(ISerpentEntry entry)
        {
            if(SendRecordsToOutput)
            {
                _directOutput.WriteEntryLine(entry);
            }
        }
    }
}
