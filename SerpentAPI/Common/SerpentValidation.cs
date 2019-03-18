using SerpentAPI.Enums;
using SerpentAPI.Exceptions;

namespace SerpentAPI.Common
{
    public static class SerpentValidation
    {
        public static void ValidateSeverityRange(EntrySeverity min, EntrySeverity max)
        {
            if(min > max)
            {
                throw new InvalidSeverityRangeException();
            }
        } 
    }
}