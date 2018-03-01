using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MrRondon.Entities;
using MrRondon.Helpers;
using MrRondon.Pages.Account;
using MrRondon.Services;
using MrRondon.Services.Rest;
using MrRondon.ViewModels;
using Newtonsoft.Json;

namespace MrRondon.Auth
{
    public class AccountManager
    {
        public AccountManager()
        {
            SetUserToken();
        }

        public User User { get; private set; }
        public TokenVm Token { get; private set; }
        public bool IsLoggedIn { get; private set; }

        private void SetUserToken()
        {
            var userToken = ApplicationManager<UserTokenVm>.Find("userToken");
            if (userToken == null)
            {
                IsLoggedIn = false;
                return;
            }

            User = userToken.User;
            Token = userToken.Token;
            IsLoggedIn = true;
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

        public static class DefaultSetting
        {
            public static City City = new City { CityId = 1, Name = "Porto Velho" };
            public static string TelephoneSetur = "(69) 3216-1044";
            public static double Latitude = -8.7592547;
            public static double Longitude = -63.8769227;
        }

        public static async Task<bool> Signin()
        {
            var service = new UserService();//todo do login by telephone number
            var isAuthenticated = await service.Authenticate(new LoginPageModel
            {
                UserName = "111.111.111-11",
                Password = "111111"
            });
            if (isAuthenticated) throw new Exception("Usuário ou senha incorreta");

            return true;
        }

        public static async Task<City> SetActualCity()
        {
            var position = await GeolocatorHelper.GetCurrentPositionAsync();

            var cityService = new CityService();
            var cityName = await cityService.GetCityName(position.Latitude, position.Longitude);

            cityName = string.IsNullOrWhiteSpace(cityName) ? DefaultSetting.City.Name : cityName;

            var cities = await cityService.GetCityAsync(cityName.Trim());
            var city = cities ?? DefaultSetting.City;
            ApplicationManager<City>.AddOrUpdate("city", city);

            return city;
        }
    }
}