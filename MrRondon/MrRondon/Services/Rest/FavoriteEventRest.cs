using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MrRondon.Auth;
using MrRondon.Entities;
using MrRondon.Helpers;

namespace MrRondon.Services.Rest
{
    public class FavoriteEventRest : BaseRest
    {
        public async Task<IList<FavoriteEvent>> GetAsync()
        {
            var url = $"{UrlService}/user/event/favorites";
            var content = await GetObjectAsync<IList<FavoriteEvent>>(url);

            return content;
        }

        public async Task<bool> IsFavorite(Guid eventId)
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.TokenType, Account.Current.Token.AccessToken);
            var url = $"{UrlService}/event/isfavorite/{eventId}";
            var isfavorite = await GetAsync<bool>(url);

            return isfavorite;
        }

        public async Task<bool> FavoriteAsync(Guid eventId)
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.TokenType, Account.Current.Token.AccessToken);

            var url = $"{UrlService}/user/event/{eventId}/favorite";
            var result = await PostAsync<bool>(url, null);

            return result;
        }
    }
}