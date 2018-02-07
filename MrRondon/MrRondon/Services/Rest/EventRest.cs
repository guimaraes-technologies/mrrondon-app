using System.Collections.Generic;
using System.Threading.Tasks;
using MrRondon.Entities;

namespace MrRondon.Services.Rest
{
   public  class EventRest : BaseRest
    {
        public async Task<IList<Event>> GetNearbyAsync(int precision, double latitude, double longitude)
        {
            var url = $"{UrlService}/event/nearby/meters/{precision}/latitude/{latitude}/longitude/{longitude}";
            var content = await GetObjectAsync<IList<Event>>(url);

            return content;
        }

        public async Task<IList<Event>> GetAsync(int cityId, string search)
        {
            var url = $"{UrlService}/event/city/{cityId}/{search}";
            var content = await GetObjectAsync<IList<Event>>(url);

            return content;
        }
    }
}