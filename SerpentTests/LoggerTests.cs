using System;
using Xunit;
using SerpentLogger;
using SerpentAPI.Models;

namespace SerpentTests
{
    public class UnitTest1
    {
        [Fact]
        public void TestRecordIsRecorded()
        {
            var serpent = new SerpentKernel();
            var newEntry = new SerpentEntry("test", EntrySeverity.High);

            serpent.Record(newEntry);

            var records = serpent.GetRecords();
        }
    }
}
