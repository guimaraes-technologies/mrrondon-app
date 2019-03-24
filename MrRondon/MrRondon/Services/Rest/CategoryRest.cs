using MrRondon.Helpers;
using MrRondon.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrRondon.Services.Rest
{
    public class CategoryRest : BaseRest
    {
        public async Task<CustomReturn<IList<CategoryListVm>>> GetAsync(string search = "")
        {
            var url = $"{UrlService}/category/{search}";
            var result = await GetObjectAsync<IList<CategoryListVm>>(url);

            return result;
        }
    }
}