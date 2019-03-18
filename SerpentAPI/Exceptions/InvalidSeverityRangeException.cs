using System;

namespace SerpentAPI.Exceptions
{
    public class InvalidSeverityRangeException : Exception
    {
        public InvalidSeverityRangeException() : base(Message())
        {

        }

        private new static string Message()
        {
            return $"Miniumum severity property cannot be larger than maximum severity property.";
        }
    }
}