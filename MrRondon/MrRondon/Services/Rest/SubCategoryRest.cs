using System.Collections.Generic;
using System.Threading.Tasks;
using MrRondon.Entities;

namespace MrRondon.Services.Rest
{
    public class SubCategoryRest : BaseRest
    {
        public async Task<IList<SubCategory>> GetAsync(int categoryId, string search)
        {
            var url = $"{UrlService}/subcategory/{categoryId}/{search}";
            var content = await GetObjectAsync<IList<SubCategory>>(url);

            return content;
        }
    }
}