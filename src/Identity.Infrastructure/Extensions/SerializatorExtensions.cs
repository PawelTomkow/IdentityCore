using Identity.Infrastructure.Commands;
using Newtonsoft.Json;

namespace Identity.Infrastructure.Extensions
{
    public static class SerializatorExtensions
    {
        public static string ToJSON(this ICommand command)
        {
            return JsonConvert.SerializeObject(command);
        }
    }
}