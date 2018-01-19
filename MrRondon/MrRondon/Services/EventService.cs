using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MrRondon.Entities;

namespace MrRondon.Services
{
    public class EventService
    {
        public async Task<IList<Event>> GetAsync(string search = "")
        {
            var service = new EventService();
            var items = await service.GetAsync(search);

            return items;
        }
    }
}