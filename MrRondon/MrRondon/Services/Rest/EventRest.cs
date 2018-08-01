using MrRondon.Auth;
using MrRondon.Entities;
using MrRondon.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MrRondon.Services.Rest
{
    public class EventRest : BaseRest
    {
        public async Task<Event> GetAsync(Guid eventId)
        {
            var url = $"{UrlService}/event/{eventId}";
            var account = Account.Current;
            if(account.IsValid)
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.TokenType, account.Token.AccessToken);
            }
            var content = await GetObjectAsync<Event>(url);

            return content;
        }

        public async Task<IList<Event>> GetAsync(int cityId, string search, int skip, int take)
        {
            var url = $"{UrlService}/event/city/{cityId}/{skip}/{take}/{search}";

            var content = await GetObjectAsync<IList<Event>>(url);

            return content;
        }
    }
}