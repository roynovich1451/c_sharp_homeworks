using System;
using System.Runtime.Serialization;

namespace HW1
{
    [Serializable]
    internal class WrongIsbnException : Exception
    {
        public WrongIsbnException()
        {
        }

        public WrongIsbnException(string message) : base(message)
        {
        }

        public WrongIsbnException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongIsbnException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}