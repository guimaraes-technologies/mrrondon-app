using System.Collections.Generic;
using System.Threading.Tasks;
using MrRondon.Entities;
using MrRondon.Helpers;
using MrRondon.Services.Rest;

namespace MrRondon.Services
{
    public class EventService
    {
        public async Task<IList<Event>> GetNearbyAsync()
        {
            var service = new EventRest();
            var currentPosition = await GeolocatorHelper.GetCurrentPositionAsync();
            var items = await service.GetNearbyAsync(Constants.GetPlacesUntil, currentPosition.Latitude, currentPosition.Longitude);

            return items;
        }

        public async Task<IList<Event>> GetAsync(string search = "")
        {
            var service = new EventRest();
            var items = await service.GetAsync(search);

            return items;
        }
    }
}