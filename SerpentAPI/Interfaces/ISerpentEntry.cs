using System;
using SerpentAPI.Enums;

namespace SerpentAPI.Interfaces
{
    public interface ISerpentEntry
    {
        DateTimeOffset Date { get; }
        EntrySeverity Severity { get; }
        string Origin { get; }
        string Message { get; }
    }
}