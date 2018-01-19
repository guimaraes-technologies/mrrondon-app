using System.Collections.Generic;
using System.Threading.Tasks;
using MrRondon.Entities;

namespace MrRondon.Services
{
    public class CategoryService
    {
        public async Task<IList<Category>> Get(string search = "")
        {
            var service = new CategoryService();
            var items = await service.Get(search);

            return items;
        }
    }
}