using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrRondon.Auth;
using MrRondon.Entities;
using MrRondon.Services.Rest;

namespace MrRondon.Services
{
    public class FavoriteEventService
    {
        public async Task<bool> FavoriteAsync(Guid eventId)
        {
            var account = Account.Current;
            if (!account.IsLoggedIn) throw new Exception("Para marcar um evento como favorito é necessário fazer login no aplicativo.");

            if (account.IsLoggedIn && account.IsTokenExpired)
                throw new Exception("O seu login expirou, por favor, faça o login novamente.");

            var service = new FavoriteEventRest();
            var isFavorite = await service.FavoriteAsync(eventId);

            return isFavorite;
        }

        public async Task<bool> IsFavorite(Guid eventId)
        {
            var service = new FavoriteEventRest();
            var isFavorite = await service.IsFavorite(eventId);

            return isFavorite;
        }

        public async Task<IList<FavoriteEvent>> GetAsync()
        {
            var service = new FavoriteEventRest();
            var items = await service.GetAsync();

            return items.OrderBy(o => o.Event.Name).ToList();
        }
    }
}