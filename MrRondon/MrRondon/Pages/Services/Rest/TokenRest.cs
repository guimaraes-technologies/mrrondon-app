using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MrRondon.Exceptions;
using MrRondon.Helpers;
using MrRondon.Pages.Account;
using MrRondon.ViewModels;
using Newtonsoft.Json;
using Plugin.Connectivity;

namespace MrRondon.Services.Rest
{
    public class TokenRest
    {
        private static readonly Uri UrlService = new Uri(Constants.Host);
        private readonly HttpClient _httpClient = new HttpClient { BaseAddress = UrlService };

        public async Task<TokenVm> Login(LoginPageModel login)
        {
            var loginInformation = new Dictionary<string, string>
            {
                    {"grant_type", "password"},
                    {"client_id", Constants.ClientId},
                    {"client_secret", Constants.ClientSecret},
                    {"username", login.UserName},
                    {"password", login.Password}
            };

            if (!CrossConnectivity.Current.IsConnected) throw new WithOutInternetConnectionException();

            var response = await _httpClient.PostAsync("security/token", new FormUrlEncodedContent(loginInformation));
            if (response.IsSuccessStatusCode && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TokenVm>(content);
            }

            await BaseRest.GenerateError(response);
            return null;
        }
    }
}