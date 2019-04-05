using System;
using SerpentAPI.Common;
using SerpentAPI.Enums;
using SerpentAPI.Interfaces;
using SerpentAPI.Models;
using SerpentKernel;
using SerpentKernel.Abstractions;
using SerpentKernel.Services;

namespace SerpentLogger
{
    public class Logger
    {
        public bool OutputLogs { get { return Kernel.SendRecordsToOutput; } set { Kernel.SendRecordsToOutput = value; } }
        public EntrySeverity MinimumSeverity { get { return Kernel.MinimumSeverity; } }
        public EntrySeverity MaximumSeverity { get { return Kernel.MaximumSeverity; } }

        private Kernel Kernel { get; set; }
        private EntryStringSerializer Serializer { get; set; }
        private FileFlusher Flusher { get; set; }

        public Logger()
        {
            InitializeSerializer();
            Kernel = SetupKernel();
            Log("Logger Initialized Succesfully!");
        }

        /// <summary> Write an entry to log record buffer
        /// <param name="message">String message to log</param>
        /// <param name="severity">Log entry severity</param>
        /// </summary>
        public void Log(string message, EntrySeverity severity = EntrySeverity.Informational)
        {
            Kernel.Record(new SerpentEntry(message, severity));
        }

        /// <summary> Save logged entries to file system
        /// <param name="path">Log file path to save</param>
        /// </summary>
        public void WriteLogs(string path)
        {
            Flusher.TargetFilePath = path;
            Kernel.FlushRecords();
        }

        /// <summary> Set logging severity filter range
        /// <param name="minimum">Lowest severity level to log</param>
        /// <param name="maximum">Highest severity level to log</param>
        /// </summary>
        public void SetSeverityRange(EntrySeverity minimum, EntrySeverity maximum)
        {
            SerpentValidation.ValidateSeverityRange(minimum, maximum);

            Kernel.MinimumSeverity = minimum;
            Kernel.MaximumSeverity = maximum;
        }

        private Kernel SetupKernel()
        {
            InitializeFlusher();
            var output = InitializeOutput();
            return new Kernel(Flusher, output);
        }

        private void InitializeFlusher()
        {
            var fileSystem = new FileSystem();
            Flusher = new FileFlusher(Serializer, fileSystem);
        }

        private void InitializeSerializer()
        {
            Serializer = new EntryStringSerializer();
        }

        private IDirectOutput InitializeOutput()
        {
            return new ConsoleOutput(Serializer);
        }
    }
}
