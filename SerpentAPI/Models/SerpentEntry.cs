using System;
using SerpentAPI.Enums;
using SerpentAPI.Interfaces;

namespace SerpentAPI.Models
{
    public class SerpentEntry : ISerpentEntry
    {
        public DateTimeOffset Date { get; }

        public EntrySeverity Severity { get; }

        public string Origin { get; set; }

        public string Message { get; set; }

        public SerpentEntry(string message, EntrySeverity severity = EntrySeverity.Informational, string origin = null)
        {
            Date = DateTimeOffset.Now;
            Severity = severity;
            Origin = origin;
            
            Message = message;
        }
    }
}