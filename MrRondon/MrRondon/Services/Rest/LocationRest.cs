using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using MrRondon.ViewModels;

namespace MrRondon.Services.Rest
{
    public class LocationRest:BaseRest
    {
        public async Task<IList<LocationVm>> NearbyAsync(int precision, double latitude, double longitude)
        {
            var url = $"{UrlService}/location/nearby/meters/{precision}/latitude/{latitude.ToString(CultureInfo.CurrentCulture).Replace(".", ",")}/longitude/{longitude.ToString(CultureInfo.CurrentCulture).Replace(".", ",")}";

            var content = await GetObjectAsync<IList<LocationVm>>(url);

            return content;
        }

    }
}