namespace Identity.Extensions
{
    public static class RequestExtensions
    {
        public static string GetToken(string value)
        {
            return value.Replace("Bearer ", "");
        }
    }
}