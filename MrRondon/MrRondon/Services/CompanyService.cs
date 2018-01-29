using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrRondon.Entities;
using MrRondon.Services.Rest;

namespace MrRondon.Services
{
    public class CompanyService
    {
        public async Task<IList<Company>> GetAsync(int segmentId, string search = "")
        {
            var service = new CompanyRest();
            var items = await service.GetAsync(segmentId, search);

            return items.OrderBy(o => o.Name).ToList();
        }

        public async Task<IList<Company>> GetAsync(int segmentId, int city, string search)
        {
            var service = new CompanyRest();
            var items = await service.GetAsync(segmentId, city, search);

            return items.OrderBy(o => o.Name).ToList();
        }
    }
}