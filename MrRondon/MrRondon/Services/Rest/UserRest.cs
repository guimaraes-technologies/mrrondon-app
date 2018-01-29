using System.Collections.Generic;
using System.Threading.Tasks;
using MrRondon.Entities;

namespace MrRondon.Services.Rest
{
    public class UserRest : BaseRest
    {
        public async Task<User> GetInformationAsync()
        {
            var url = $"{UrlService}/user/information";
            var content = await GetObjectAsync<User>(url);

            return content;
        }

        public async Task<IList<Event>> GetFavoriteEventsAsync()
        {
            var url = $"{UrlService}/user/event/favorites";
            var content = await GetObjectAsync<IList<Event>>(url);

            return content;
        }
    }
}