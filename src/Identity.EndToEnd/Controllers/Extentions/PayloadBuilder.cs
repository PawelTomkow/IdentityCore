using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Identity.EndToEnd.Controllers.Extentions
{
    public static class PayloadBuilder
    {
        public static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}