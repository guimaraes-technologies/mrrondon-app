using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MrRondon.Auth;
using MrRondon.Entities;
using MrRondon.Exceptions;
using MrRondon.Helpers;
using MrRondon.ViewModels;
using Newtonsoft.Json;
using Plugin.Connectivity;

namespace MrRondon.Services.Rest
{
    public class UserRest : BaseRest
    {
        public async Task<TokenVm> Signin(string userName, string password)
        {
            if (!CrossConnectivity.Current.IsConnected) throw new Exception("Você está sem conexão com a internet");

            var login = new Dictionary<string, string>
            {
                {"grant_type", "password"},
                {"client_id", Constants.ClientId},
                {"client_secret", Constants.ClientSecret},
                {"username", userName},
                {"password", password}
            };

            var response = await HttpClient.PostAsync("security/token", new FormUrlEncodedContent(login));
            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TokenVm>(content);
            }

            await GenerateError(response);
            throw new Exception("Não foi possível concluir a requisição");
        }

        public async Task<UserTokenVm> Register(RegisterVm register)
        {
            if (!CrossConnectivity.Current.IsConnected) throw new Exception("Você está sem conexão com a internet");

            var jsonObject = JsonConvert.SerializeObject(register);

            var response = await HttpClient.PostAsync($"{UrlService}/user/register", new StringContent(jsonObject, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserTokenVm>(json);
            }
            var errors = await response.Content.ReadAsStringAsync();

            throw new Exception(errors);
        }

        public async Task<User> GetInformationAsync(string token)
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.TokenType, token);
            var url = $"{UrlService}/user/information";
            var content = await GetObjectAsync<User>(url);

            return content;
        }

        public async Task<FavoriteEvent> GetAsync(FavoriteEvent favoriteEvent)
        {
            if (!CrossConnectivity.Current.IsConnected) throw new WithOutInternetConnectionException();

            var jsonObject = JsonConvert.SerializeObject(favoriteEvent);

            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.TokenType, AccountManager.Token.AccessToken);
            var result = await PostObjectAsync<FavoriteEvent>($"{UrlService}/event/favorite", content);

            return result;
        }

        public async Task<IList<Event>> GetFavoriteEventsAsync()
        {
            var url = $"{UrlService}/user/event/favorites";
            var content = await GetObjectAsync<IList<Event>>(url);

            return content;
        }
    }
}