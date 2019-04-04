using System;
using Xunit;
using SerpentLogger;
using SerpentAPI.Models;
using SerpentAPI.Enums;
using System.Linq;
using SerpentAPI.Exceptions;
using SerpentAPI.Interfaces;
using Moq;

namespace SerpentTests.Tests.Kernel
{
    public class RecordSeverityLevelTests
    {

        [Fact]
        public void PassRecordWhenSeverityHigherThanMinimum()
        {
            var outputMock = new Mock<IDirectOutput>();
            var flushMock = new Mock<IRecordFlusher>();
            var serpent = new SerpentLogger.Kernel(flushMock.Object, outputMock.Object);
            var newEntry = new SerpentEntry(String.Empty, EntrySeverity.Critical);

            serpent.MinimumSeverity = EntrySeverity.High;
            serpent.Record(newEntry);

            Assert.NotEmpty(serpent.GetRecords());
        }

        [Fact]
        public void FailRecordWhenSeverityLowerThanMinimum()
        {
            var outputMock = new Mock<IDirectOutput>();
            var flushMock = new Mock<IRecordFlusher>();
            var serpent = new SerpentLogger.Kernel(flushMock.Object, outputMock.Object);
            var newEntry = new SerpentEntry(String.Empty, EntrySeverity.Medium);

            serpent.MinimumSeverity = EntrySeverity.High;
            serpent.Record(newEntry);

            Assert.Empty(serpent.GetRecords());
        }

        [Fact]
        public void PassRecordWhenSeverityLowerThanMaximum()
        {
            var outputMock = new Mock<IDirectOutput>();
            var flushMock = new Mock<IRecordFlusher>();
            var serpent = new SerpentLogger.Kernel(flushMock.Object, outputMock.Object);
            var newEntry = new SerpentEntry(String.Empty, EntrySeverity.Medium);

            serpent.MaximumSeverity = EntrySeverity.High;
            serpent.Record(newEntry);

            Assert.NotEmpty(serpent.GetRecords());
        }

        [Fact]
        public void FailRecordWhenSeverityHigherThanMaximum()
        {
            var outputMock = new Mock<IDirectOutput>();
            var flushMock = new Mock<IRecordFlusher>();
            var serpent = new SerpentLogger.Kernel(flushMock.Object, outputMock.Object);
            var newEntry = new SerpentEntry(String.Empty, EntrySeverity.Critical);

            serpent.MaximumSeverity = EntrySeverity.High;
            serpent.Record(newEntry);

            Assert.Empty(serpent.GetRecords());
        }

        [Fact]
        public void PassRecordWhenSeverityIsMinimum()
        {
            var outputMock = new Mock<IDirectOutput>();
            var flushMock = new Mock<IRecordFlusher>();
            var serpent = new SerpentLogger.Kernel(flushMock.Object, outputMock.Object);
            var newEntry = new SerpentEntry(String.Empty, EntrySeverity.Informational);

            serpent.MinimumSeverity = EntrySeverity.Informational;
            serpent.Record(newEntry);

            Assert.NotEmpty(serpent.GetRecords());
        }

        [Fact]
        public void PassRecordWhenSeverityIsMaximum()
        {
            var outputMock = new Mock<IDirectOutput>();
            var flushMock = new Mock<IRecordFlusher>();
            var serpent = new SerpentLogger.Kernel(flushMock.Object, outputMock.Object);
            var newEntry = new SerpentEntry(String.Empty, EntrySeverity.Critical);

            serpent.MaximumSeverity = EntrySeverity.Critical;
            serpent.Record(newEntry);

            Assert.NotEmpty(serpent.GetRecords());
        }

        [Fact]
        public void ThrowOnInvalidSeverityRange()
        {
            var outputMock = new Mock<IDirectOutput>();
            var flushMock = new Mock<IRecordFlusher>();
            var serpent = new SerpentLogger.Kernel(flushMock.Object, outputMock.Object);
            var newEntry = new SerpentEntry(String.Empty);
            
            serpent.MinimumSeverity = EntrySeverity.Critical;
            serpent.MaximumSeverity = EntrySeverity.Low;

            Assert.Throws<InvalidSeverityRangeException>( () => serpent.Record(newEntry));
        }

        [Fact]
        public void PassSingleSeverityRangeValue()
        {
            var outputMock = new Mock<IDirectOutput>();
            var flushMock = new Mock<IRecordFlusher>();
            var serpent = new SerpentLogger.Kernel(flushMock.Object, outputMock.Object);
            var newEntry = new SerpentEntry(String.Empty,EntrySeverity.Critical);
            
            serpent.MinimumSeverity = EntrySeverity.Critical;
            serpent.MaximumSeverity = EntrySeverity.Critical;
            serpent.Record(newEntry);

            Assert.NotEmpty(serpent.GetRecords());
        }
    }
}
