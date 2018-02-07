using System.Linq;
using System.Threading.Tasks;
using MrRondon.Entities;
using MrRondon.Services;

namespace MrRondon.Auth
{
    public class AccountManager
    {
        public AccountManager()
        {
            SetUser();
        }

        public User User { get; private set; }
        public bool IsLoggedIn { get; private set; }
        public City City { get; private set; }

        private void SetUser()
        {
            var userService = new UserService();
            var user = userService.GetLocal();
            User = user;
            IsLoggedIn = user != null;
        }

        public async Task SetCity(string cityName)
        {
            var cityService = new CityService();
            var cities = await cityService.GetAsync(cityName);
            if (cities != null && cities.Any()) City = cities.FirstOrDefault();
        }
    }
}