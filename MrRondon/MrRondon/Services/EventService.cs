using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MrRondon.Entities;

namespace MrRondon.Services
{
    public class EventService
    {
        public async Task<IList<Event>> Get(string search = "")
        {
            var service = new EventService();
            return await service.Get(search);

            var items = new List<Event>
            {
                new Event
                {
                    EventId = Guid.NewGuid(),
                    Name = "Corrida de Ganso"
                }
            };

            await Task.Delay(10);

            return items;
        }
    }
}