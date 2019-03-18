using System;
using Xunit;
using SerpentLogger;
using SerpentAPI.Models;
using SerpentAPI.Enums;
using System.Linq;

namespace SerpentTests
{
    public class LoggerTests
    {

        [Fact]
        public void PassRecordWhenSeverityHigherThanMinimum()
        {
            var serpent = new SerpentKernel();
            var newEntry = new SerpentEntry(String.Empty, EntrySeverity.Critical);

            serpent.MinimumSeverity = EntrySeverity.High;
            serpent.Record(newEntry);

            var records = serpent.GetRecords();
            var recordCount = records.Count();
            var isRecorded = recordCount == 1;

            Assert.True(isRecorded);
        }

        [Fact]
        public void FailRecordWhenSeverityLowerThanMinimum()
        {
            var serpent = new SerpentKernel();
            var newEntry = new SerpentEntry(String.Empty, EntrySeverity.Medium);

            serpent.MinimumSeverity = EntrySeverity.High;
            serpent.Record(newEntry);

            var records = serpent.GetRecords();
            var recordCount = records.Count();
            var isRecorded = recordCount == 1;


            Assert.False(isRecorded);
        }

        [Fact]
        public void PassRecordWhenSeverityLowerThanMaximum()
        {
            var serpent = new SerpentKernel();
            var newEntry = new SerpentEntry(String.Empty, EntrySeverity.Medium);

            serpent.MaximumSeverity = EntrySeverity.High;
            serpent.Record(newEntry);

            var records = serpent.GetRecords();
            var recordCount = records.Count();
            var isRecorded = recordCount == 1;

            Assert.True(isRecorded);
        }

        [Fact]
        public void FailRecordWhenSeverityHigherThanMaximum()
        {
            var serpent = new SerpentKernel();
            var newEntry = new SerpentEntry(String.Empty, EntrySeverity.Critical);

            serpent.MaximumSeverity = EntrySeverity.High;
            serpent.Record(newEntry);

            var records = serpent.GetRecords();
            var recordCount = records.Count();
            var isRecorded = recordCount == 1;

            Assert.False(isRecorded);
        }
    }
}
