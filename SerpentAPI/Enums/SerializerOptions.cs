using System;

namespace SerpentAPI.Enums
{
    [Flags]
    public enum SerializerOptions
    {
        IncludeDate = 1,
        IncludeSeverity = 2,
        IncludeOrigin = 4,
        IncludeMessage = 8
    }
}