using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrRondon.Auth;
using MrRondon.Services.Rest;
using MrRondon.ViewModels;
using Position = Plugin.Geolocator.Abstractions.Position;

namespace MrRondon.Services
{
    public class LocationService
    {
        public async Task<IList<LocationVm>> NearbyAsync(Position  currentPosition)
        {
            var service = new LocationRest();
            var precision = AccountManager.GetPrecision();

            var items = await service.NearbyAsync(precision, currentPosition.Latitude, currentPosition.Longitude);

            return items.OrderBy(o => o.Label).ToList();
        }

    }
}