using System.Collections.Generic;
using System.Threading.Tasks;
using MrRondon.Entities;

namespace MrRondon.Services.Rest
{
    public class CityRest : BaseRest
    {
        public async Task<IList<City>> Search(string search)
        {
            var url = $"{UrlService}/city/{search}";
            var content = await GetObjectAsync<IList<City>>(url);

            return content;
        }
    }
}