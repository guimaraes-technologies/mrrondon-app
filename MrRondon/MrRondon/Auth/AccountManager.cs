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

        public City City { get; private set; } = new City { CityId = 1, Name = "Porto Velho" };

        private void SetUser()
        {
            if (Xamarin.Forms.Application.Current.Properties.ContainsKey("user"))
            {
                User = Xamarin.Forms.Application.Current.Properties["user"] as User;
                IsLoggedIn = true;
            }
            else IsLoggedIn = false;
        }

        private void SetCity(City city)
        {
            if (city == null) return;

            if (Xamarin.Forms.Application.Current.Properties.ContainsKey("city"))
                Xamarin.Forms.Application.Current.Properties.Remove("city");
            Xamarin.Forms.Application.Current.Properties["city"] = City = city;
        }

        public async Task SetCity(string cityName)
        {
            var cityService = new CityService();
            var cities = await cityService.GetAsync(cityName);
            SetCity(cities.FirstOrDefault());
        }
    }
}