using System;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace MrRondon.Helpers
{
    public class GeolocatorHelper
    {
        public static async Task<Position> GetCurrentPosition()
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
    }
}