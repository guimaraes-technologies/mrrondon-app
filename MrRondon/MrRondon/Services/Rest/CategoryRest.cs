using System.Collections.Generic;
using System.Threading.Tasks;
using MrRondon.Entities;

namespace MrRondon.Services.Rest
{
    public class CategoryRest : BaseRest
    {
        public async Task<IList<SubCategory>> GetAsync(string search)
        {
            var url = $"{UrlService}/category/{search}";
            var content = await GetObjectAsync<IList<SubCategory>>(url);

            return content;
        }
    }
}