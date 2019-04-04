using Xunit;
using Moq;
using SerpentKernel.Interfaces;
using SerpentKernel.Services;
using System.Collections.Generic;
using SerpentAPI.Interfaces;
using SerpentAPI.Models;
using SerpentKernel.Enums;

namespace SerpentTests.Tests.Services
{
    public class FileFlusherTests
    {
        [Fact]
        public void PassAfterFlushingFilledBufferOverwriting()
        {
            var ser = new Mock<EntryStringSerializer>();
            var fs = new Mock<IFileSystem>();
            var flusher = new FileFlusher(ser.Object, fs.Object);
            var itms = new List<ISerpentEntry>() { new SerpentEntry("One"), new SerpentEntry("Two") };

            flusher.TargetFileOptions = FileFlusherOptions.Overwrite;
            flusher.SetRecordBuffer(itms);
            flusher.FlushRecordBuffer();

            fs.Verify(x => x.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void PassAfterFlushingFilledBufferAppending()
        {
            var ser = new Mock<EntryStringSerializer>();
            var fs = new Mock<IFileSystem>();
            var flusher = new FileFlusher(ser.Object, fs.Object);
            var itms = new List<ISerpentEntry>() { new SerpentEntry("One"), new SerpentEntry("Two") };

            flusher.TargetFileOptions = FileFlusherOptions.Append;
            flusher.SetRecordBuffer(itms);
            flusher.FlushRecordBuffer();

            fs.Verify(x => x.AppendAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void PassFlushingSingleEntry()
        {
            var ser = new Mock<EntryStringSerializer>();
            var fs = new Mock<IFileSystem>();
            var flusher = new FileFlusher(ser.Object, fs.Object);
            var item = new SerpentEntry("One");

            flusher.FlushSingleEntry(item);
            flusher.FlushSingleEntry(item);

            fs.Verify(x => x.AppendAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
        }
    }
}