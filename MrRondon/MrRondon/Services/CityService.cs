using MrRondon.Entities;
using MrRondon.Helpers;
using MrRondon.Services.Rest;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrRondon.Services
{
    public class CityService
    {
        //public async Task<IList<City>> GetHasCompanyAsync(int subCategoryId)
        //{
        //    var service = new CityRest();
        //    var items = await service.GetHasCompanyAsync(subCategoryId);

        //    ApplicationManager<string>.AddOrUpdate("cities", items);
        //    return items.OrderBy(o => o.Name).ToList();
        //}

        //public async Task<IList<City>> GetHasEventAsync()
        //{ 
        //    var service = new CityRest();
        //    var items = await service.GetHasEventAsync();

        //    ApplicationManager<string>.AddOrUpdate("cities", items);
        //    return items.OrderBy(o => o.Name).ToList();
        //}

        //public async Task<IList<City>> GetHasHistoricalSightAsync()
        //{
        //    var service = new CityRest();
        //    var items = await service.GetHasHistoricalSightAsync();

        //    ApplicationManager<string>.AddOrUpdate("cities", items);
        //    return items.OrderBy(o => o.Name).ToList(); 
        //}
    }
}