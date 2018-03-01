using System;
using Newtonsoft.Json;

namespace MrRondon.ViewModels
{
    public class TokenVm
    {
        public string UserName { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty(".issued")]
        public DateTimeOffset Issued { get; set; }

        [JsonProperty(".expires")]
        public static DateTimeOffset Expires { get; set; }
    }
}