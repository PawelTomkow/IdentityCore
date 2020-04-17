using System;

namespace Identity.Extensions
{
    public static class RequestExtension
    {
        public static string GenerateIdRequest()
        {
            return $"{Guid.NewGuid().ToString()}";
        }
    }
}