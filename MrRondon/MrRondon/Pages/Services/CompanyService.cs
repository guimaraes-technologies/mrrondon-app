using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrRondon.Entities;
using MrRondon.Helpers;
using MrRondon.Services.Rest;

namespace MrRondon.Services
{
    public class CompanyService
    {
        public async Task<IList<Company>> GetAsync(int categoryId, int segmentId, string search = "", int skip = 0, int take = Constants.Pagination.Take)
        {
            var service = new CompanyRest();
            var items = await service.GetAsync(categoryId, segmentId, search, skip, take);

            return items.OrderBy(o => o.Name).ToList();
        }

        public async Task<Company> GetByIdAsync(Guid id)
        {
            var service = new CompanyRest();
            var item = await service.GetByIdAsync(id);

            return item;
        }
    }
}