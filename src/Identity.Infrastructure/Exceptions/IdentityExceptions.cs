using System;
using System.Runtime.Serialization;

namespace Identity.Infrastructure.Exceptions
{
    public class IdentityExceptions : Exception
    {
        public IdentityExceptions()
        {
        }

        protected IdentityExceptions(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public IdentityExceptions(string message) : base(message)
        {
        }

        public IdentityExceptions(string message, Exception innerException) : base(message, innerException)
        {
        }

        public IdentityExceptions(string code, string message) : base(message)
        {
        }
    }
}