using System;

namespace SerpentAPI.Exceptions
{
    public class InvalidSerializerOptionsException : Exception
    {
        
        public InvalidSerializerOptionsException() : base(Message())
        {

        }

        private new static string Message()
        {
            return "Serializer options cannot be empty";
        }
    }
}