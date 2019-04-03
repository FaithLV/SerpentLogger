using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SerpentAPI.Interfaces;
using SerpentLogger.Enums;

namespace SerpentLogger.Services
{
    public class FileFlusher : IRecordFlusher
    {

        public string TargetFilePath { get; set; }
        public FileFlusherOptions TargetFileOptions { get; set; }

        private ISerpentEntry[] RecordBuffer { get; set; }
        private readonly EntryStringSerializer _serializer;

        public FileFlusher(EntryStringSerializer serializer)
        {
            _serializer = serializer;
            TargetFilePath = "./log.log";
            TargetFileOptions = FileFlusherOptions.Overwrite;
        }

        public void FlushRecordBuffer()
        {
            var serializerBuffer = GetSerializedBuffer();
            WriteToFile(serializerBuffer);
            RecordBuffer = null;
        }

        public void FlushSingleEntry(ISerpentEntry entry)
        {
            var serialized = _serializer.Serialize(entry);
            AppendToFile(serialized);
        }

        public void SetRecordBuffer(IEnumerable<ISerpentEntry> record)
        {
            RecordBuffer = record.ToArray();
        }

        private string GetSerializedBuffer()
        {
            var writableBuffer = new StringBuilder();
            for(int i = 0; i < RecordBuffer.Length; i++)
            {
                var serialized = _serializer.Serialize(RecordBuffer[i]);
                writableBuffer.Append(serialized);
                writableBuffer.Append(System.Environment.NewLine);
            }

            return writableBuffer.ToString();
        }

        private void WriteToFile(string buffer)
        {
            if(TargetFileOptions == FileFlusherOptions.Append)
            {
                AppendToFile(buffer);
                return;
            }

            OverwriteFile(buffer);
        }

        private void AppendToFile(string buffer)
        {
            File.AppendAllText(TargetFilePath, buffer);
        }

        private void OverwriteFile(string buffer)
        {
            File.WriteAllText(TargetFilePath, buffer);
        }
    }
}