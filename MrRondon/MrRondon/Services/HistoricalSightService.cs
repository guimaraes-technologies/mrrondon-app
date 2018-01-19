using System.Collections.Generic;
using System.Threading.Tasks;
using MrRondon.Entities;
using MrRondon.Services.Rest;

namespace MrRondon.Services
{
    public class HistoricalSightService
    {
        public async Task<IList<HistoricalSight>> Get(string search = "")
        {
            var service = new HistoricalSightRest();
            var items = await service.Get(search);

            return items;
        }
    }
}