using System.Collections.Generic;
using System.Threading.Tasks;
using MrRondon.Entities;
using MrRondon.Services.Rest;

namespace MrRondon.Services
{
    public class HistoricalSightService
    {
        public async Task<IList<HistoricalSight>> GetAsync(string search = "")
        {
            var service = new HistoricalSightRest();
            var items = await service.GetAsync(search);

            return items;
        }
    }
}