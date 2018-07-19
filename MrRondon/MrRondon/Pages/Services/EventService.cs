using MrRondon.Entities;
using MrRondon.Helpers;
using MrRondon.Services.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrRondon.Services
{
    public class EventService
    {
        public async Task<Event> GetAsync(Guid eventId)
        {
            var service = new EventRest();
            var item = await service.GetAsync(eventId);

            return item;
        }

        public async Task<IList<Event>> GetAsync(int cityId, string search = "", int skip = 0, int take = Constants.Pagination.Take)
        {
            var service = new EventRest();
            var items = await service.GetAsync(cityId, search, skip, take);

            return items.OrderBy(o => o.Name).ToList();
        }
    }
}