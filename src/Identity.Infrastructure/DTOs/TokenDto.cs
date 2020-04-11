using System.Collections.Generic;
using Identity.Infrastructure.Commands;
using Newtonsoft.Json;

namespace Identity.Infrastructure.DTOs
{
    public class TokenDto : ICommand
    {
        [JsonProperty("idUser")] public int IdUser { get; set; }

        [JsonProperty("idSession")] public string IdSession { get; set; }

        [JsonProperty("token")] public string Token { get; set; }

        [JsonProperty("refreshToken")] public string RefreshToken { get; set; }

        [JsonProperty("experienceTime")] public long ExperienceTime { get; set; }

        [JsonProperty("claims")] public List<string> Claims { get; set; }
    }
}