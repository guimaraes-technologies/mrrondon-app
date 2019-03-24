using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrRondon.Entities;
using MrRondon.Helpers;
using MrRondon.Services.Rest;

namespace MrRondon.Services
{
    public class HistoricalSightService
    {
        public async Task<CustomReturn<HistoricalSight>> GetByIdAsync(int id)
        {
            var service = new HistoricalSightRest();
            var result = await service.GetByIdAsync(id);

            return result;
        }
    }
}