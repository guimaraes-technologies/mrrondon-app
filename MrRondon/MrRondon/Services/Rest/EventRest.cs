using System.Collections.Generic;
using System.Threading.Tasks;
using MrRondon.Entities;

namespace MrRondon.Services.Rest
{
   public  class EventRest : BaseRest
    {
        public async Task<IList<Event>> GetAsync(string search)
        {
            var url = $"{UrlService}/event/{search}";
            var content = await GetObjectAsync<IList<Event>>(url);

            return content;
        }
    }
}