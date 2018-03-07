using System.Collections.Generic;
using Newtonsoft.Json;

namespace MrRondon.Helpers
{
    public class ApplicationManager<TObject> where TObject : class
    {
        public static TObject Find(string key)
        {
            var properties = Xamarin.Forms.Application.Current.Properties;
            if (!Exist(key)) return null;

            var jsonObject = properties[key].ToString();
            var value = JsonConvert.DeserializeObject<TObject>(jsonObject);

            return value;
        }

        public static void AddOrUpdate(string key, object value)
        {
            var jsonObject = JsonConvert.SerializeObject(value);
            if (Exist(key)) Remove(key);
            Xamarin.Forms.Application.Current.Properties.Add(new KeyValuePair<string, object>(key, jsonObject));
        }

        //public static void AddOrUpdate(string key, string json)
        //{
        //    if (Exist(key)) Remove(key);
        //    Xamarin.Forms.Application.Current.Properties.Add(new KeyValuePair<string, object>(key, json));
        //}

        public static void Remove(string key)
        {
            Xamarin.Forms.Application.Current.Properties.Remove(key);
        }

        private static bool Exist(string key)
        {
            return Xamarin.Forms.Application.Current.Properties.ContainsKey(key);
        }
    }
}