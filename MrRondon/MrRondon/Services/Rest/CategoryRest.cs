using System.Collections.Generic;
using System.Threading.Tasks;
using MrRondon.ViewModels;

namespace MrRondon.Services.Rest
{
    public class CategoryRest : BaseRest
    {
        public async Task<IList<CategoryListVm>> GetAsync(string search)
        {
            var url = $"{UrlService}/category/{search}";
            var content = await GetObjectAsync<IList<CategoryListVm>>(url);

            return content;
        }
    }
}