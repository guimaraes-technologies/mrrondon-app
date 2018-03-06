using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrRondon.Auth;
using MrRondon.Entities;
using MrRondon.Helpers;
using MrRondon.Services.Rest;

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

        public async Task<IList<Event>> GetNearbyAsync()
        {
            var service = new EventRest();
            var currentPosition = await GeolocatorHelper.GetCurrentPositionAsync();
            if (currentPosition == null) return new List<Event>();

            var items = await service.GetNearbyAsync(AccountManager.DefaultSetting.PlaceUntil, currentPosition.Latitude, currentPosition.Longitude);

            return items.OrderBy(o => o.Name).ToList();
        }

        public async Task<IList<Event>> GetAsync(int cityId, string search = "")
        {
            var service = new EventRest();
            var items = await service.GetAsync(cityId, search);

            return items.OrderBy(o => o.Name).ToList();
        }
    }
}