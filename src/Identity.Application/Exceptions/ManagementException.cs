using System;
using System.Runtime.Serialization;

namespace Identity.Application.Exceptions
{
    public class ManagementException : Exception
    {
        public ManagementException()
        {
        }

        protected ManagementException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ManagementException(string message) : base(message)
        {
        }

        public ManagementException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}