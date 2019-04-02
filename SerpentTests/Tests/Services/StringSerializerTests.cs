using System;
using System.Collections.Generic;
using SerpentAPI.Enums;
using SerpentAPI.Exceptions;
using SerpentAPI.Models;
using SerpentLogger.Services;
using Xunit;

namespace SerpentTests.Tests.Services
{
    public class StringSerializerTests
    {
        [Fact]
        public void PassWhenListEntriesSerialized()
        {
            var serializer = new EntryStringSerializer();
            var entryList = new List<SerpentEntry>() 
            { 
                new SerpentEntry("Message One"), 
                new SerpentEntry("Message Two"),
                new SerpentEntry("Message Three")
            };

            var serialized = serializer.Serialize(entryList);

            Assert.Equal(entryList.Count, serialized.Count);
        }

        [Fact]
        public void PassWithAllFlagsSerializedAsExpected()
        {
            var serializer = new EntryStringSerializer();
            serializer.DateFormat = "mm:ss";
            serializer.Options = SerializerOptions.IncludeDate | SerializerOptions.IncludeMessage | SerializerOptions.IncludeOrigin | SerializerOptions.IncludeSeverity;

            var entry = new SerpentEntry("Hi!", EntrySeverity.Informational, "Unit Test");

            var serialized = serializer.Serialize(entry);
            System.Console.WriteLine(serialized);

            Assert.Contains(entry.Date.ToString(serializer.DateFormat), serialized);
            Assert.Contains(entry.Message ,serialized);
            Assert.Contains(entry.Severity.ToString() ,serialized);
            Assert.Contains(entry.Origin ,serialized);
        }

        [Fact]
        public void ThrowWhenSerializeWithNoOptions()
        {
            var serializer = new EntryStringSerializer();
            serializer.Options = 0;

            var entry = new SerpentEntry(String.Empty);
            Assert.Throws<InvalidSerializerOptionsException>(() => serializer.Serialize(entry));
        }

        [Fact]
        public void PassWhenSingleFlagSerializedAsExpected()
        {
            var serializer = new EntryStringSerializer();
            serializer.Options = SerializerOptions.IncludeMessage;
            var entry = new SerpentEntry("This is a message!", EntrySeverity.High, "Unit Test");

            var serialized = serializer.Serialize(entry);

            Assert.Contains(entry.Message, serialized);
            Assert.DoesNotContain(entry.Origin, serialized);
            Assert.DoesNotContain(entry.Severity.ToString(), serialized);
            Assert.DoesNotContain(entry.Date.ToString(serializer.DateFormat), serialized);
        }

    }
}