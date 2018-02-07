using System;
using System.Linq;
using System.Threading.Tasks;
using MrRondon.Extensions;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms.Maps;
using Position = Plugin.Geolocator.Abstractions.Position;

namespace MrRondon.Helpers
{
    public class GeolocatorHelper
    {
        public static async Task<Position> GetCurrentPositionAsync()
        {
            Position position = null;
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                position = await locator.GetLastKnownLocationAsync();

                //got a cahched position, so let's use it.
                if (position != null) return position;

                //not available or not enabled
                if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled) return null;

                position = await locator.GetPositionAsync(TimeSpan.FromMilliseconds(1000), null, true);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                //Display error as we have timed out or can't get location.
            }

            return position;
        }

        public static async Task<string> GetCurrentCityAsync(double latitude, double longitude)
        {
            var geocoder = new Geocoder();
            var position = new Xamarin.Forms.Maps.Position(latitude, longitude);
            var possibleAddresses = await geocoder.GetAddressesForPositionAsync(position);

            var address = possibleAddresses?.ToArray()[1];
            var city = address?.Replace("- RO, Brasil", "").Trim();
            return city ?? "Porto Velho";
        }


        // Metodo que retorna a distancia entre locais num raio de X metros
        public static double PlacesAround(Position actualPosition, Position place)
        {
            const int earthRadius = 6371;
            //calcula o raio de distancia entre os dois pontos
            var latitude = (place.Latitude - actualPosition.Latitude).ToRadian();
            var longitude = (place.Longitude - actualPosition.Longitude).ToRadian();
            //Usa a formula de Haversine para conferir as posicoes geograficas dos pontos no globo terreste
            var tmp = (Math.Sin(latitude / 2) * Math.Sin(latitude / 2)) +
                         (Math.Cos(actualPosition.Latitude.ToRadian()) * Math.Cos(place.Latitude.ToRadian()) *
                          Math.Sin(longitude / 2) * Math.Sin(longitude / 2));
            var c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(tmp)));
            var d = earthRadius * c;
            return d * 1000;
        }
    }
}