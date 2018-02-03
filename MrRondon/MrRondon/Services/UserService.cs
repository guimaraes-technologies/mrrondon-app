using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrRondon.Entities;
using MrRondon.Repository;
using MrRondon.Services.Rest;

namespace MrRondon.Services
{
    public class UserService
    {
        private readonly UserRepository _repo = new UserRepository();

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

        public User GetLocal()
        {
            var user = _repo.GetLocal();
            return user;
        }
    }
}