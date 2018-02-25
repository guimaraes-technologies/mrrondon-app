using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrRondon.Auth;
using MrRondon.Entities;
using MrRondon.Helpers;
using MrRondon.Services.Rest;

namespace MrRondon.Services
{
    public class CompanyService
    {
        public async Task<IList<Company>> GetAsync(int categoryId, string search = "")
        {
            var service = new CompanyRest();
            var items = await service.GetAsync(categoryId, AccountManager.DefaultSetting.City.CityId, search);

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