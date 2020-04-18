using System.Collections.Generic;
using Identity.Application.Commands;
using Newtonsoft.Json;

namespace Identity.Application.DTOs
{
    public class TokenDto : ICommand
    {
        [JsonProperty("tokenId")] public int TokenId { get; set; }
        [JsonProperty("idSession")] public string IdSession { get; set; }

        [JsonProperty("accessToken")] public string AccessToken { get; set; }

        [JsonProperty("refreshToken")] public string RefreshToken { get; set; }

        [JsonProperty("experienceTime")] public long ExperienceTime { get; set; }

        [JsonProperty("claims")] public List<string> Claims { get; set; }
    }
}