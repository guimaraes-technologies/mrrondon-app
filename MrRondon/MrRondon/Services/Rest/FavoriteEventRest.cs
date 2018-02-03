using System.Collections.Generic;
using System.Threading.Tasks;
using MrRondon.Entities;

namespace MrRondon.Services.Rest
{
    public class FavoriteEventRest : BaseRest
    {
        public async Task<IList<FavoriteEvent>> GetAsync()
        {
            var url = $"{UrlService}/user/event/favorites";
            var content = await GetObjectAsync<IList<FavoriteEvent>>(url);

            return content;
        }

        public async Task<FavoriteEvent> MarkAsFavoriteAsync(FavoriteEvent model)
        {
            var url = $"{UrlService}/user/event/favorite";
            var content = await GetObjectAsync<FavoriteEvent>(url);

            return content;
        }

        public async Task<FavoriteEvent> UnMarkAsFavoriteAsync(FavoriteEvent model)
        {
            var url = $"{UrlService}/user/event/unfavorite";
            var content = await GetObjectAsync<FavoriteEvent>(url);

            return content;
        }
    }
}