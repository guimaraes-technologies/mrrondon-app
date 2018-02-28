using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrRondon.Entities;
using MrRondon.Services.Rest;

namespace MrRondon.Services
{
    public class CityService
    {
        public async Task<IList<City>> GetAsync(int subCategoryId)
        {
            var service = new CityRest();
            var items = await service.GetAsync(subCategoryId);

            return items.OrderBy(o => o.Name).ToList();
        }

        public async Task<City> GetCityAsync(string search)
        {
            var service = new CityRest();
            var city = await service.GetCityAsync(search);

            return city;
        }

        public async Task<string> GetCityName(double latitude, double longitude)
        {
            var service = new CityRest();
            var cityName = await service.GetCityName(latitude, longitude);

            return cityName;
        }
    }
}