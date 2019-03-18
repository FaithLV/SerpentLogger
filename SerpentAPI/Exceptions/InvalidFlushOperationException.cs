using System;

namespace SerpentAPI.Exceptions
{
    public class InvalidFlushOperationException : Exception
    {
        
        public InvalidFlushOperationException() : base(Message())
        {

        }

        private new static string Message()
        {
            return $"Cannot manually flush record buffer when 'ForceFlushOnRecord=true'";
        }
    }
}