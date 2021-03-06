﻿using System;
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
        public async Task<CustomReturn<IList<FavoriteEvent>>> GetAsync()
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.TokenType, Account.Current.Token.AccessToken);
            var url = $"{UrlService}/event/favorites";
            var content = await GetObjectAsync<IList<FavoriteEvent>>(url);

            return content;
        }

        public async Task<CustomReturn<bool>> IsFavorite(Guid eventId)
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.TokenType, Account.Current.Token.AccessToken);
            var url = $"{UrlService}/event/isfavorite/{eventId}";
            var isfavorite = await GetAsync<bool>(url);

            return isfavorite;
        }

        public async Task<CustomReturn<bool>> FavoriteAsync(Guid eventId)
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.TokenType, Account.Current.Token.AccessToken);

            var url = $"{UrlService}/event/{eventId}/favorite";
            var result = await PostAsync<bool>(url, null);

            return result;
        }
    }
}