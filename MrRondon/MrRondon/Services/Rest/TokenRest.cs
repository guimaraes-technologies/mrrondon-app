using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Fastick.ViewModels;
using MrRondon.Helpers;
using Newtonsoft.Json;
using Plugin.Connectivity;

namespace MrRondon.Services.Rest
{
    public class TokenRest
    {
        private static readonly Uri UrlService = new Uri(Constants.Host);
        private readonly HttpClient _httpClient = new HttpClient { BaseAddress = UrlService };

        public async Task<TokenVm> Login(string login, string password)
        {
            var loginInformation = new Dictionary<string, string>
            {
                    {"grant_type", "password"},
                    //{"client_id", Constants.ClientId},
                    //{"client_secret", Constants.ClientSecret},
                    {"username", login},
                    {"password", password}
            };

            if (!CrossConnectivity.Current.IsConnected) throw new Exception("Você está sem conexão com a internet");

            var response = await _httpClient.PostAsync("seguranca/login", new FormUrlEncodedContent(loginInformation));
            if (response.IsSuccessStatusCode && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TokenVm>(content);
            }

            await BaseRest.ShowError(response);
            throw new Exception("Não foi possível concluir a requisição");
        }
    }
}