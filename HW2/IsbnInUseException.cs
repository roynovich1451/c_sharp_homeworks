using System;
using System.Runtime.Serialization;

namespace HW1wpfApp
{
    [Serializable]
    internal class IsbnInUseException : Exception
    {
        public IsbnInUseException()
        {
        }

        public IsbnInUseException(string message) : base(message)
        {
        }

        public IsbnInUseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IsbnInUseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}