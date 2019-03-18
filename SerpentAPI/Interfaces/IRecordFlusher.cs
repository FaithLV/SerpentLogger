using System.Collections.Generic;

namespace SerpentAPI.Interfaces
{
    public interface IRecordFlusher
    {
        void SetRecordBuffer(IEnumerable<ISerpentEntry> record);
        void FlushRecordBuffer();
        void FlushSingleEntry(ISerpentEntry entry);
    }
}