using System;
using Newtonsoft.Json;

namespace MrRondon.ViewModels
{
    public class TokenVm
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty(".expires")]
        public DateTimeOffset Expires { get; set; }
    }
}