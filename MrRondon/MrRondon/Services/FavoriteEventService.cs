using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MrRondon.Auth;
using MrRondon.Entities;
using MrRondon.Helpers;
using MrRondon.Services.Rest;

namespace MrRondon.Services
{
    public class FavoriteEventService
    {
        public async Task<CustomReturn<bool>> FavoriteAsync(Guid eventId)
        {
            var account = Account.Current;
            if (!account.IsLoggedIn) throw new Exception("Para marcar um evento como favorito é necessário fazer login no aplicativo.");

            if (account.IsLoggedIn && account.IsTokenExpired) 
                throw new Exception("O seu login expirou, por favor, faça o login novamente.");

            var service = new FavoriteEventRest();
            var result = await service.FavoriteAsync(eventId);
            
            return result;
        }

        public async Task<CustomReturn<bool>> IsFavorite(Guid eventId)
        {
            var service = new FavoriteEventRest();
            var isFavorite = await service.IsFavorite(eventId);

            return isFavorite;
        }

        public async Task<CustomReturn<IList<FavoriteEvent>>> GetAsync()
        {
            var service = new FavoriteEventRest();
            var items = await service.GetAsync();

            return items;
        }
    }
}