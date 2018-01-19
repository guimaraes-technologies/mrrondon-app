using System.Collections.Generic;
using System.Threading.Tasks;
using MrRondon.Entities;
using MrRondon.Services.Rest;

namespace MrRondon.Services
{
    public class CityService
    {
        public async Task<IList<City>> GetAsync(string search = "")
        {
            var service = new CityRest();
            var items = await service.GetAsync(search);

            return items;
        }
    }
}