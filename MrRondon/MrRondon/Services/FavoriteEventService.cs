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
        public async Task<IList<FavoriteEvent>> GetAsync()
        {
            var service = new FavoriteEventRest();
            var items = await service.GetAsync();

            return items.OrderBy(o => o.Event.Name).ToList();
        }

        public async Task<FavoriteEvent> MarkAsFavoriteAsync(Guid eventId)
        {
            var user = Auth.Account.Current.User;
            var favoriteEvent = new FavoriteEvent
            {
                EventId = eventId,
                UserId = user.UserId
            };
            var service = new FavoriteEventRest();
            var item = await service.MarkAsFavoriteAsync(favoriteEvent);

            return item;
        }
    }
}