using System.Collections.Generic;
using System.Threading.Tasks;
using MrRondon.Entities;

namespace MrRondon.Services.Rest
{
    public class CategoryRest : BaseRest
    {
        public async Task<IList<Category>> Search(string search = "")
        {
            var url = $"{UrlService}/category/{search}";
            var content = await GetObjectAsync<IList<Category>>(url);

            return content;
        }
    }
}