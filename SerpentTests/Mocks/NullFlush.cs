using System.Collections.Generic;
using SerpentAPI.Interfaces;

namespace SerpentTests.Mocks
{
    public class NullFlush : IRecordFlusher
    {
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