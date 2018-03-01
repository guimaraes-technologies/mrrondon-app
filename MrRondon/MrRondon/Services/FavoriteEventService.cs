using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrRondon.Entities;
using MrRondon.Services.Rest;

namespace MrRondon.Services
{
    public class FavoriteEventService
    {
        public async Task<bool> FavoriteAsync(Guid eventId)
        {
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