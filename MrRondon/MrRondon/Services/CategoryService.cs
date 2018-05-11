using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrRondon.Services.Rest;
using MrRondon.ViewModels;

namespace MrRondon.Services
{
    public class CategoryService
    {
        public async Task<IList<CategoryListVm>> GetAsync(string search = "")
        {
            var service = new CategoryRest();
            var items = await service.GetAsync(search);

            return items.OrderBy(o => o.Name).ToList();
        }
    }
}