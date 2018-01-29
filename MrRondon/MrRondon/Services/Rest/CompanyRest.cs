using System.Collections.Generic;
using System.Threading.Tasks;
using MrRondon.Entities;

namespace MrRondon.Services.Rest
{
    public class CompanyRest : BaseRest
    {
        public async Task<IList<Company>> GetAsync(int segmentId, string search)
        {
            var url = $"{UrlService}/company/{segmentId}/{search}";
            var content = await GetObjectAsync<IList<Company>>(url);

            return content;
        }
    }
}