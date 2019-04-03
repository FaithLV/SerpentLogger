using System.Collections.Generic;
using SerpentAPI.Interfaces;

namespace SerpentLogger.Services
{
    public class FileFlusher : IRecordFlusher
    {
        public string TargetFilePath { get; set; }
        private List<ISerpentEntry> RecordBuffer { get; set; }

        public FileFlusher()
        {

        }

        public void FlushRecordBuffer()
        {

        }

        public void FlushSingleEntry(ISerpentEntry entry)
        {

        }

        public void SetRecordBuffer(IEnumerable<ISerpentEntry> record)
        {

        }
    }
}