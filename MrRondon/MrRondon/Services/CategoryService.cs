using System.Collections.Generic;
using System.Threading.Tasks;
using MrRondon.Entities;
using MrRondon.Services.Rest;

namespace MrRondon.Services
{
    public class CategoryService
    {
        public async Task<IList<Category>> GetAsync(string search = "")
        {
            var service = new CategoryRest();
            var items = await service.GetAsync(search);

            return items;
        }
    }
}