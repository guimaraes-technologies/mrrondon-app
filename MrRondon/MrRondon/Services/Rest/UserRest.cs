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
        public async Task<TokenVm> Signin(string userName, string password)
        {
            ValidateConnection();

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
                var json = await httpResponse.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TokenVm>(json);
                return result;
            }

            var error = await GenerateError(httpResponse);
            throw error;
        }

        public async Task<User> Register(RegisterPageModel register)
        {
            ValidateConnection();

            var jsonObject = JsonConvert.SerializeObject(register);

            var httpResponse = await HttpClient.PostAsync($"{UrlService}/user/register", new StringContent(jsonObject, Encoding.UTF8, "application/json"));
            
            if (httpResponse.IsSuccessStatusCode && httpResponse.StatusCode == HttpStatusCode.OK)
            {
                var json = await httpResponse.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<User>(json);
                return result;
            }

            var error = await GenerateError(httpResponse);
            throw error;
        }

        public async Task<User> GetInformationAsync(string token)
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.TokenType, token);
            var url = $"{UrlService}/user/information";
            var content = await GetObjectAsync<User>(url);

            return content;
        }

        public async Task<IList<Event>> GetFavoriteEventsAsync()
        {
            var url = $"{UrlService}/user/event/favorites";
            var content = await GetObjectAsync<IList<Event>>(url);

            return content;
        }
    }
}