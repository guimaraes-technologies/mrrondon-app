using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrRondon.Auth;
using MrRondon.Helpers;
using MrRondon.Services.Rest;
using MrRondon.ViewModels;

namespace MrRondon.Services
{
    public class LocationService
    {

        public async Task<IList<LocationVm>> NearbyAsync()
        {
            var service = new LocationRest();
            var currentPosition = await GeolocatorHelper.GetCurrentPositionAsync();
            if (currentPosition == null) return new List<LocationVm>();

            var items = await service.NearbyAsync(AccountManager.DefaultSetting.PlaceUntilOption, currentPosition.Latitude, currentPosition.Longitude);

            return items.OrderBy(o => o.Label).ToList();
        }

    }
}