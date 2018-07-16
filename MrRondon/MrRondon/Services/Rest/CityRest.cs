using MrRondon.Auth;
using MrRondon.Entities;
using MrRondon.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MrRondon.Services.Interfaces;
using Xamarin.Forms;

namespace MrRondon.Services.Rest
{
    public class CityRest : BaseRest
    {
        public async Task<string> GetCityName(double latitude, double longitude)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var result = await client.GetAsync($"https://maps.googleapis.com/maps/api/geocode/json?latlng={latitude},{longitude}&key={Constants.GoogleKey}");

                    if (!result.IsSuccessStatusCode) return AccountManager.DefaultSetting.City.Name;

                    var json = await result.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<dynamic>(json);
                    var cityName = obj.results[0].address_components[3].long_name;
                    return cityName;
                }
            }
            catch (Exception ex)
            {
                var exceptionService = DependencyService.Get<IExceptionService>();
                exceptionService.TrackError(ex.Message);
                return AccountManager.DefaultSetting.City.Name;
            }
        }

        public async Task<IList<City>> GetHasCompanyAsync(int subCategoryId)
        {
            var url = $"{UrlService}/city/has/company/subcategory/{subCategoryId}";
            var content = await GetObjectAsync<IList<City>>(url);

            return content;
        }

        public async Task<IList<City>> GetHasEventAsync()
        {
            var url = $"{UrlService}/city/has/event";
            var content = await GetObjectAsync<IList<City>>(url);

            return content;
        }

        public async Task<IList<City>> GetHasHistoricalSightAsync()
        {
            var url = $"{UrlService}/city/has/historicalsight";
            var content = await GetObjectAsync<IList<City>>(url);

            return content;
        }

        public async Task<IList<City>> GetAsync(string name)
        {
            var url = $"{UrlService}/city/{name}";
            var content = await GetObjectAsync<IList<City>>(url);

            return content;
        }

        public async Task<City> GetCityAsync(string search)
        {
            var url = $"{UrlService}/city/first/{search}";
            var content = await GetObjectAsync<City>(url);

            return content;
        }
    }
}