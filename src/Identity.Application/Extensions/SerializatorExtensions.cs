using Identity.Application.Commands;
using Newtonsoft.Json;

namespace Identity.Application.Extensions
{
    public static class SerializatorExtensions
    {
        public static string ToJSON(this ICommand command)
        {
            return JsonConvert.SerializeObject(command);
        }
    }
}