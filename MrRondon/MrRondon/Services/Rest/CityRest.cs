using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using MrRondon.Auth;
using MrRondon.Entities;
using MrRondon.Helpers;
using Newtonsoft.Json;

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
                Debug.WriteLine(ex.Message);
                return AccountManager.DefaultSetting.City.Name;
            }
        }

        public async Task<IList<City>> GetAsync(int subCategoryId)
        {
            var url = $"{UrlService}/city/subcategory/{subCategoryId}";
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
            var url = $"{UrlService}/city/{search}";
            var content = await GetObjectAsync<City>(url);

            return content;
        }
    }
}