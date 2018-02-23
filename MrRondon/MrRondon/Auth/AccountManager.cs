using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrRondon.Entities;
using MrRondon.Helpers;
using MrRondon.Services;
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

        public static class Token
        {
            public static string AccessToken => ApplicationManager<string>.Find(nameof(AccessToken));
        }

        public static async Task<bool> Signin()
        {
            var service=new UserRest();//todo do login by telephone number
            var token = await service.Signin("111.111.111-11", "111111");
            if (token == null) throw new Exception("Usuário ou senha incorreta");

            var json = JsonConvert.SerializeObject(token);
            ApplicationManager<string>.AddOrUpdate("token", json);
            return true;
        }

        public static async Task<City> SetActualCity()
        {
            var position = await GeolocatorHelper.GetCurrentPositionAsync();

            var cityService = new CityService();
            var cityName = await cityService.GetCityName(position.Latitude, position.Longitude);

            cityName = string.IsNullOrWhiteSpace(cityName) ? Constants.DefaultSetting.City.Name : cityName;

            var cities = await cityService.GetCityAsync(cityName.Trim());
            var city = cities ?? Constants.DefaultSetting.City;
            ApplicationManager<City>.AddOrUpdate("city", city);

            return city;
        }
    }
}