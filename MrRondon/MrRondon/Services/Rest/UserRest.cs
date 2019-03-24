using MrRondon.Entities;
using MrRondon.Helpers;
using MrRondon.Pages.Account;
using MrRondon.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MrRondon.Services.Rest
{
    public class UserRest : BaseRest
    {
        public async Task<CustomReturn<TokenVm>> Signin(string userName, string password)
        {
            var resultConnection = await ValidateConnection();
            if (!resultConnection.IsValid) return new CustomReturn<TokenVm>(resultConnection.Error);

            var login = new Dictionary<string, string>
            {
                {"grant_type", "password"},
                {"client_id", Constants.ClientId},
                {"client_secret", Constants.ClientSecret},
                {"username", userName},
                {"password", password}
            };
            var httpResponse = await HttpClient.PostAsync("security/token", new FormUrlEncodedContent(login));

            if (httpResponse.IsSuccessStatusCode && httpResponse.StatusCode == HttpStatusCode.OK)
            {
                string json = await httpResponse.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<TokenVm>(json);
                return new CustomReturn<TokenVm>(result);
            }

            var error = await GetError(httpResponse);
            return new CustomReturn<TokenVm>(error);
        }

        public async Task<CustomReturn<User>> Register(RegisterPageModel register)
        {
            var resultConnection = await ValidateConnection();
            if (!resultConnection.IsValid) return new CustomReturn<User>(resultConnection.Error);

            var jsonObject = JsonConvert.SerializeObject(register);

            var httpResponse = await HttpClient.PostAsync($"{UrlService}/user/register", new StringContent(jsonObject, Encoding.UTF8, "application/json"));

            if (httpResponse.IsSuccessStatusCode && httpResponse.StatusCode == HttpStatusCode.OK)
            {
                var json = await httpResponse.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<User>(json);

                return new CustomReturn<User>(result);
            }

            var error = await GetError(httpResponse);
            return new CustomReturn<User>(error);
        }

        public async Task<CustomReturn<User>> GetInformationAsync(string token)
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.TokenType, token);
            var url = $"{UrlService}/user/information";
            var content = await GetObjectAsync<User>(url);

            return content;
        }

        public async Task<CustomReturn<IList<Event>>> GetFavoriteEventsAsync()
        {
            var url = $"{UrlService}/user/event/favorites";
            var content = await GetObjectAsync<IList<Event>>(url);

            return content;
        }
    }
}