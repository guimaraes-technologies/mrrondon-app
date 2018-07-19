using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrRondon.Entities;
using MrRondon.Services.Rest;

namespace MrRondon.Services
{
    public class SubCategoryService
    {
        public async Task<IList<SubCategory>> GetAsync(int category, string search = "")
        {
            var service = new SubCategoryRest();
            var items = await service.GetAsync(category, search);

            return items.OrderBy(o => o.Name).ToList();
        }
    }
}