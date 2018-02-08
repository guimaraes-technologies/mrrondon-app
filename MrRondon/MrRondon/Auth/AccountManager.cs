using System.Linq;
using System.Threading.Tasks;
using MrRondon.Entities;
using MrRondon.Services;
using Xamarin.Forms;

namespace MrRondon.Auth
{
    public class AccountManager
    {
        public AccountManager()
        {
            SetUser();
            //SetCity();
        }

        public User User { get; private set; }
        public bool IsLoggedIn { get; private set; }

        public City City { get; private set; }

        private void SetUser()
        {
            if (Application.Current.Properties.ContainsKey("user"))
            {
                User = Application.Current.Properties["user"] as User;
                IsLoggedIn = true;
            }
            else IsLoggedIn = false;
        }

        private void SetCity(City city = null)
        {
            var defaultCity = new City { CityId = 1, Name = "Porto Velho" };
            var properties = Application.Current.Properties;

            if (properties.ContainsKey("city") && city != null)
                Application.Current.Properties["city"] = City = city;
            else properties["city"] = City = defaultCity;
        }

        public async Task<City> SetCityAsync(string cityName)
        {
            var cityService = new CityService();
            var cities = await cityService.GetAsync(cityName);
            var city = cities.FirstOrDefault();
            //SetCity(city);

            return city;
        }
    }
}