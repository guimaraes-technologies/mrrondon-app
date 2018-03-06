using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MrRondon.Entities;
using MrRondon.Helpers;
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
        public bool IsTokenExpired { get; private set; }
        public bool IsValid { get; private set; }

        private void SetUserToken()
        {
            var userToken = ApplicationManager<UserTokenVm>.Find("userToken");
            if (userToken == null)
            {
                IsLoggedIn = false;
                IsTokenExpired = true;
                IsValid = IsLoggedIn && !IsTokenExpired;
                return;
            }
            User = userToken.User;
            Token = userToken.Token;
            IsLoggedIn = true;
            IsTokenExpired = Token.Expires < DateTimeOffset.Now;
            IsValid = IsLoggedIn && !IsTokenExpired;
        }

        public static async Task<IList<City>> GetAsync()
        {
            var rest = new CityRest();
            var cities = await rest.GetAsync(string.Empty) ?? GetLocalCities();

            SetLocalCities(cities);
            return cities;
        }

        public static async Task<IList<City>> GetHasCompanyAsync(int subCategoryId)
        {
            var rest = new CityRest();
            var cities = await rest.GetHasCompanyAsync(subCategoryId) ?? GetLocalCities();

            SetLocalCities(cities);
            return cities;
        }

        public static async Task<IList<City>> GetHasEventAsync()
        {
            var rest = new CityRest();
            var cities = await rest.GetHasEventAsync() ?? GetLocalCities();

            SetLocalCities(cities);
            return cities;
        }

        public static async Task<IList<City>> GetHasHistoricalSightAsync()
        {
            var rest = new CityRest();
            var cities = await rest.GetHasHistoricalSightAsync() ?? GetLocalCities();

            SetLocalCities(cities);
            return cities;
        }

        public static void SetLocalCities(IList<City> cities)
        {
            ApplicationManager<string>.AddOrUpdate("cities", cities);
        }

        public static IList<City> GetLocalCities()
        {
            var localCities = ApplicationManager<IList<City>>.Find("cities");

            return localCities;
        }

        public static class DefaultSetting
        {
            public static City City = new City { CityId = 1, Name = "Porto Velho" };
            public static string TelephoneSetur = "6932161044";
            public static string EmailSetur = "contato@ozielguimaraes.net";
            public static double Latitude = -8.7592547;
            public static double Longitude = -63.8769227;
            public static int PlaceUntil = 1000;
        }

        public static async Task<City> SetActualCity()
        {
            var position = await GeolocatorHelper.GetCurrentPositionAsync();

            var cityService = new CityService();
            var cityName = await cityService.GetCityName(position.Latitude, position.Longitude);

            cityName = string.IsNullOrWhiteSpace(cityName) ? DefaultSetting.City.Name : cityName;

            var cities = await cityService.GetByNameAsync(cityName.Trim());
            var city = cities ?? DefaultSetting.City;
            ApplicationManager<City>.AddOrUpdate("city", city);

            return city;
        }
    }
}