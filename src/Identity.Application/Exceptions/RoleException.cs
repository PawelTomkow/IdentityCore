using System;
using System.Runtime.Serialization;

namespace Identity.Application.Services
{
    public class RoleException : Exception
    {
        public RoleException()
        {
        }

        protected RoleException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public RoleException(string message) : base(message)
        {
        }

        public RoleException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}