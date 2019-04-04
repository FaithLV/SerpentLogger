using System.Collections.Generic;
using System.Linq;
using System.Text;
using SerpentAPI.Interfaces;
using SerpentKernel.Enums;
using SerpentKernel.Interfaces;

namespace SerpentKernel.Services
{
    public class FileFlusher : IRecordFlusher
    {
        public string TargetFilePath { get; set; }
        public FileFlusherOptions TargetFileOptions { get; set; }

        private ISerpentEntry[] RecordBuffer { get; set; }
        private readonly EntryStringSerializer _serializer;
        private readonly IFileSystem _fileSystem;

        public FileFlusher(EntryStringSerializer serializer, IFileSystem fileSystem)
        {
            _serializer = serializer;
            _fileSystem = fileSystem;

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
            _fileSystem.AppendAllText(TargetFilePath, buffer);
        }

        private void OverwriteFile(string buffer)
        {
            _fileSystem.WriteAllText(TargetFilePath, buffer);
        }
    }
}