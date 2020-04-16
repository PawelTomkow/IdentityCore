using System;
using System.Runtime.Serialization;

namespace Identity.Infrastructure.Exceptions
{
    public class RepositoryException : Exception
    {
        public RepositoryException()
        {
        }

        protected RepositoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public RepositoryException(string message) : base(message)
        {
        }

        public RepositoryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}