using System;
using Xunit;
using SerpentLogger;
using SerpentAPI.Models;
using SerpentAPI.Enums;
using System.Linq;
using SerpentAPI.Exceptions;

namespace SerpentTests.Tests.Kernel
{
    public class RecordSeverityLevelTests
    {

        [Fact]
        public void PassRecordWhenSeverityHigherThanMinimum()
        {
            var serpent = new SerpentKernel();
            var newEntry = new SerpentEntry(String.Empty, EntrySeverity.Critical);

            serpent.MinimumSeverity = EntrySeverity.High;
            serpent.Record(newEntry);

            Assert.NotEmpty(serpent.GetRecords());
        }

        [Fact]
        public void FailRecordWhenSeverityLowerThanMinimum()
        {
            var serpent = new SerpentKernel();
            var newEntry = new SerpentEntry(String.Empty, EntrySeverity.Medium);

            serpent.MinimumSeverity = EntrySeverity.High;
            serpent.Record(newEntry);

            Assert.Empty(serpent.GetRecords());
        }

        [Fact]
        public void PassRecordWhenSeverityLowerThanMaximum()
        {
            var serpent = new SerpentKernel();
            var newEntry = new SerpentEntry(String.Empty, EntrySeverity.Medium);

            serpent.MaximumSeverity = EntrySeverity.High;
            serpent.Record(newEntry);

            Assert.NotEmpty(serpent.GetRecords());
        }

        [Fact]
        public void FailRecordWhenSeverityHigherThanMaximum()
        {
            var serpent = new SerpentKernel();
            var newEntry = new SerpentEntry(String.Empty, EntrySeverity.Critical);

            serpent.MaximumSeverity = EntrySeverity.High;
            serpent.Record(newEntry);

            Assert.Empty(serpent.GetRecords());
        }

        [Fact]
        public void PassRecordWhenSeverityIsMinimum()
        {
            var serpent = new SerpentKernel();
            var newEntry = new SerpentEntry(String.Empty, EntrySeverity.Informational);

            serpent.MinimumSeverity = EntrySeverity.Informational;
            serpent.Record(newEntry);

            Assert.NotEmpty(serpent.GetRecords());
        }

        [Fact]
        public void PassRecordWhenSeverityIsMaximum()
        {
            var serpent = new SerpentKernel();
            var newEntry = new SerpentEntry(String.Empty, EntrySeverity.Critical);

            serpent.MaximumSeverity = EntrySeverity.Critical;
            serpent.Record(newEntry);

            Assert.NotEmpty(serpent.GetRecords());
        }

        [Fact]
        public void ThrowOnInvalidSeverityRange()
        {
            var serpent = new SerpentKernel();
            var newEntry = new SerpentEntry(String.Empty);
            
            serpent.MinimumSeverity = EntrySeverity.Critical;
            serpent.MaximumSeverity = EntrySeverity.Low;

            Assert.Throws<InvalidSeverityRangeException>( () => serpent.Record(newEntry));
        }

        [Fact]
        public void PassSingleSeverityRangeValue()
        {
            var serpent = new SerpentKernel();
            var newEntry = new SerpentEntry(String.Empty,EntrySeverity.Critical);
            
            serpent.MinimumSeverity = EntrySeverity.Critical;
            serpent.MaximumSeverity = EntrySeverity.Critical;
            serpent.Record(newEntry);

            Assert.NotEmpty(serpent.GetRecords());
        }
    }
}
