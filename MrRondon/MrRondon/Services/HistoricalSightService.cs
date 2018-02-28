using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrRondon.Auth;
using MrRondon.Entities;
using MrRondon.Services.Rest;

namespace MrRondon.Services
{
    public class HistoricalSightService
    {
        public async Task<IList<HistoricalSight>> GetAsync(int cityId, string search = "")
        {
            var service = new HistoricalSightRest();
            var items = await service.GetAsync(cityId, search);
            
            return items.OrderBy(o => o.Name).ToList();
        }

        public async Task<HistoricalSight> GetByIdAsync(int id)
        {
            var service = new HistoricalSightRest();
            var item = await service.GetByIdAsync(id);

            return item;
        }
    }
}