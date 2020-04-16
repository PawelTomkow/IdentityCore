namespace Identity.Application.Extensions
{
    public static class StringExtension
    {
        public static bool Empty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
    }
}