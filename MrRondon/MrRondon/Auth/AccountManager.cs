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
            SetCity();
        }

        public User User { get; private set; }
        public bool IsLoggedIn { get; private set; }

        public int CurrentCityId { get; private set; }
        public string CurrentCityName { get; private set; }

        private void SetUser()
        {
            if (Xamarin.Forms.Application.Current.Properties.ContainsKey("user"))
            {
                User = Xamarin.Forms.Application.Current.Properties["user"] as User;
                IsLoggedIn = true;
            }
            else IsLoggedIn = false;
        }

        private void SetCity(City city = null)
        {
            if (Xamarin.Forms.Application.Current.Properties.ContainsKey("city"))
                Xamarin.Forms.Application.Current.Properties.Remove("city");
            city = city ?? new City { CityId = 1, Name = "Porto Velho" };
            Xamarin.Forms.Application.Current.Properties["city"] = city;

            CurrentCityId = city.CityId;
            CurrentCityName = city.Name;
        }

        public async Task<City> SetCity(string cityName)
        {
            var cityService = new CityService();
            var cities = await cityService.GetAsync(cityName);
            var city = cities.FirstOrDefault();
            SetCity(city);

            return city;
        }
    }
}