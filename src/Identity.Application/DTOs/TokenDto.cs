using Identity.Application.Commands;
using Newtonsoft.Json;

namespace Identity.Application.DTOs
{
    public class TokenDto : ICommand
    {
        [JsonProperty("accessToken")] public string AccessToken { get; set; }
        [JsonProperty("refreshToken")] public string RefreshToken { get; set; }
    }
}