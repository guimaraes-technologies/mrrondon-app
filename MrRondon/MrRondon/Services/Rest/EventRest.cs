using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MrRondon.Auth;
using MrRondon.Entities;
using MrRondon.Helpers;

namespace MrRondon.Services.Rest
{
    public class EventRest : BaseRest
    {
        public async Task<Event> GetAsync(Guid eventId)
        {
            var url = $"{UrlService}/event/{eventId}";
            var token = Account.Current.Token;
            if(token.IsValid)
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.TokenType, token.AccessToken);
            }
            var content = await GetObjectAsync<Event>(url);

            return content;
        }

        public async Task<IList<Event>> GetNearbyAsync(int precision, double latitude, double longitude)
        {
            var url = $"{UrlService}/event/nearby/meters/{precision}/latitude/{latitude.ToString(CultureInfo.CurrentCulture).Replace(".", ",")}/longitude/{longitude.ToString(CultureInfo.CurrentCulture).Replace(".", ",")}";
            var content = await GetObjectAsync<IList<Event>>(url);

            return content;
        }

        public async Task<IList<Event>> GetAsync(int cityId, string search)
        {
            var url = $"{UrlService}/event/city/{cityId}/{search}";
            var content = await GetObjectAsync<IList<Event>>(url);

            return content;
        }
    }
}