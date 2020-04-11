using System;

namespace Identity.Core.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string invalidUsername, string usernameIsInvalid)
        {
            throw new NotImplementedException();
        }
    }
}