using System;
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

        public async Task<IList<Company>> GetAsync(int segmentId, int city, string search)
        {
            var cityId = city == 0 ? string.Empty : $"{city}/";
            var url = $"{UrlService}/company/{segmentId}/{cityId}{search}";
            var content = await GetObjectAsync<IList<Company>>(url);

            return content;
        }

        public async Task<Company> GetByIdAsync(Guid id)
        {
            var url = $"{UrlService}/company/{id}";
            var content = await GetObjectAsync<Company>(url);

            return content;
        }
    }
}