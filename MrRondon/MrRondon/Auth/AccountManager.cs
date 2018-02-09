using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrRondon.Entities;
using MrRondon.Helpers;
using MrRondon.Services.Rest;
using Newtonsoft.Json;
using Xamarin.Forms;

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

        private void SetUser()
        {
            if (Application.Current.Properties.ContainsKey("user"))
            {
                User = Application.Current.Properties["user"] as User;
                IsLoggedIn = true;
            }
            else IsLoggedIn = false;
        }

        public static async Task<IList<City>> GetCities()
        {
            IList<City> cities;
            var localCities = ApplicationManager<string>.Find("cities");
            if (string.IsNullOrWhiteSpace(localCities))
            {
                var rest = new CityRest();
                cities = await rest.GetAsync(string.Empty);
                var json = JsonConvert.SerializeObject(cities);
                ApplicationManager<string>.AddOrUpdate("cities", json);
            }
            else cities = JsonConvert.DeserializeObject<IList<City>>(localCities);

            return cities;
        }
    }
}