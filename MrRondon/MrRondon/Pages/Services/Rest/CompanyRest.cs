using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MrRondon.Entities;

namespace MrRondon.Services.Rest
{
    public class CompanyRest : BaseRest
    {
        public async Task<Company> GetByIdAsync(Guid id)
        {
            var url = $"{UrlService}/company/{id}";
            var content = await GetObjectAsync<Company>(url);

            return content;
        }

        public async Task<IList<Company>> GetAsync(int segmentId, int cityId, string search, int skip, int take)
        {
            var url = $"{UrlService}/company/city/{cityId}/segment/{segmentId}/{skip}/{take}/{search}";
            var content = await GetObjectAsync<IList<Company>>(url);

            return content;
        }
    }
}