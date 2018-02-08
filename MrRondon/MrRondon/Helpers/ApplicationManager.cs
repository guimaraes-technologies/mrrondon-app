using System.Collections.Generic;

namespace MrRondon.Helpers
{
    public class ApplicationManager<TObject> where TObject : class
    {
        public static TObject Find(string key)
        {
            var properties = Xamarin.Forms.Application.Current.Properties;
            if (!Exist(key)) return null;

            var value = properties[key];
            var convertedValue = value as TObject;
            return convertedValue;
        }

        public static void AddOrUpdate(string key, object value)
        {
            if (Exist(key)) Remove(key);
            Xamarin.Forms.Application.Current.Properties.Add(new KeyValuePair<string, object>(key, value));
        }

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