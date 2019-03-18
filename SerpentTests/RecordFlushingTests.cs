using System;
using Xunit;
using SerpentLogger;
using SerpentAPI.Models;
using SerpentAPI.Enums;
using System.Linq;
using SerpentTests.Mocks;
using SerpentAPI.Exceptions;

namespace SerpentTests
{
    public class RecordFlushingTests
    {
        [Fact]
        public void PassFlushWithoutBypass()
        {
            var serpent = new SerpentKernel();
            var nullFlusher = new NullFlush();
            var newEntry = new SerpentEntry(String.Empty);

            serpent.SetRecordFlusher(nullFlusher);
            serpent.ForceFlushOnRecord = false;
            serpent.Record(newEntry);

            Assert.NotEmpty(serpent.GetRecords());
            serpent.FlushRecords();
            Assert.Empty(serpent.GetRecords());
        }        

        [Fact]
        public void ThrowManualFlushWithBypass()
        {
            var serpent = new SerpentKernel();
            var nullFlusher = new NullFlush();
            var newEntry = new SerpentEntry(String.Empty);

            serpent.SetRecordFlusher(nullFlusher);
            serpent.ForceFlushOnRecord = true;

            Assert.Throws<InvalidFlushOperationException>( () => serpent.FlushRecords());
        }        
    }
}