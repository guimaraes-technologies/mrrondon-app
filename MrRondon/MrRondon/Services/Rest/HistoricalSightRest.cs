﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MrRondon.Entities;
using MrRondon.Helpers;

namespace MrRondon.Services.Rest
{
    public class HistoricalSightRest : BaseRest
    {
        public async Task<CustomReturn<IList<HistoricalSight>>> GetAsync(int cityId, string search)
        {
            var url = $"{UrlService}/historicalsight/city/{cityId}/{search}";
            var content = await GetObjectAsync<IList<HistoricalSight>>(url);

            return content;
        }

        public async Task<CustomReturn<HistoricalSight>> GetByIdAsync(int id)
        {
            var url = $"{UrlService}/historicalsight/{id}";
            var content = await GetObjectAsync<HistoricalSight>(url);

            return content;
        }
    }
}