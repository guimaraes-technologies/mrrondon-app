using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrRondon.Entities;
using MrRondon.Helpers;
using MrRondon.Services.Rest;
using Newtonsoft.Json;

namespace MrRondon.Services
{
    public class CityService
    {
        public async Task<IList<City>> GetAsync(int subCategoryId)
        {
            var service = new CityRest();
            var items = await service.GetAsync(subCategoryId);

            var json = JsonConvert.SerializeObject(items);
            ApplicationManager<string>.AddOrUpdate("cities", json);
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