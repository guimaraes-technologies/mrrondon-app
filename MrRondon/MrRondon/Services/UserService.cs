using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrRondon.Entities;
using MrRondon.Services.Rest;
using Xamarin.Forms;

namespace MrRondon.Services
{
    public class UserService
    {
        public async Task<User> GetInformationAsync()
        {
            var service = new UserRest();
            var user = await service.GetInformationAsync();

            return user;
        }

        public async Task<IList<Event>> GetFavoriteEventsAsync()
        {
            var service = new UserRest();
            var items = await service.GetFavoriteEventsAsync();

            return items.OrderBy(o => o.Name).ToList();
        }
    }
}