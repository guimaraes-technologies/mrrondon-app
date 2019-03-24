using MrRondon.Entities;
using MrRondon.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrRondon.Services.Rest
{
    public class SubCategoryRest : BaseRest
    {
        public async Task<CustomReturn<IList<SubCategory>>> GetAsync(int categoryId, string search = "")
        {
            var url = $"{UrlService}/subcategory/{categoryId}/{search}";
            var content = await GetObjectAsync<IList<SubCategory>>(url);

            return content;
        }
    }
}