﻿    using MrRondon.Entities;
using MrRondon.Extensions;
using MrRondon.Helpers;
using MrRondon.Services;
using MrRondon.Services.Interfaces;
using MrRondon.Services.Rest;
using MrRondon.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

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
            try
            {
                var rest = new CityRest();
                var result = await rest.GetAsync(string.Empty);
                if (result.IsValid)
                {
                    SetLocalCities(result.Value);
                    return result.Value;
                }
                else return GetLocalCities();
            }
            catch (Exception ex)
            {
                var exception = DependencyService.Get<IExceptionService>();
                exception.TrackError(ex, "AccountManager.GetAsync");
                return new List<City> { DefaultSetting.City };
            }
        }

        public static async Task<IList<City>> GetHasCompanyAsync(int subCategoryId)
        {
            var rest = new CityRest();
            var result = await rest.GetHasCompanyAsync(subCategoryId);
            if (result.IsValid)
            {
                SetLocalCities(result.Value);
                return result.Value;
            }
            else return GetLocalCities();
        }

        public static async Task<IList<City>> GetHasEventAsync()
        {
            var rest = new CityRest();
            var result = await rest.GetHasEventAsync();
            if (result.IsValid)
            {
                SetLocalCities(result.Value);
                return result.Value;
            }
            else return GetLocalCities();
        }

        public static async Task<IList<City>> GetHasHistoricalSightAsync()
        {
            var rest = new CityRest();

            var result = await rest.GetHasHistoricalSightAsync();
            if (result.IsValid)
            {
                SetLocalCities(result.Value);
                return result.Value;
            }
            else return GetLocalCities();
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
            public static City City = new City { CityId = 37, Name = "Porto Velho" };
            public static double Latitude = -8.7592547;
            public static double Longitude = -63.8769227;
            public static PlaceUntilOption PlaceUntilOption = PlaceUntilOption.Thousand;
        }

        public static double GetPrecision()
        {
            var until = ApplicationManager<object>.Find("PlaceUntil");

            var defaultValue = EnumExtensions.GetAttribute(DefaultSetting.PlaceUntilOption);
            return until == null ? double.Parse(defaultValue.KeyValue) : double.Parse($"{until}");
        }

        public static async Task<City> SetActualCity()
        {
            try
            {
                var position = await GeolocatorHelper.GetCurrentPositionAsync();

                var cityService = new CityRest();
                var cityName = await cityService.GetCityNameAsync(position.Latitude, position.Longitude);

                cityName = string.IsNullOrWhiteSpace(cityName) ? DefaultSetting.City.Name : cityName;

                var result = await cityService.GetCityAsync(cityName.Trim());
                var city = result.Value ?? DefaultSetting.City;
                ApplicationManager<City>.AddOrUpdate("city", city);

                return city;
            }
            catch (Exception ex)
            {
                var exception = DependencyService.Get<IExceptionService>();
                exception.TrackError(ex, "SetActualCity");
                return DefaultSetting.City;
            }
        }

        public static void Logout()
        {
            var userService = new UserService();
            userService.Logout();
        }
    }
}