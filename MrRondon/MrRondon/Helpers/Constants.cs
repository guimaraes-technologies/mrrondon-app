using MrRondon.Entities;

namespace MrRondon.Helpers
{
    public static class Constants
    {
        public static string Host => "http://api.mrrondon.ozielguimaraes.net";
        // public static string Host => "http://localhost:1111";
        public const string AppName = "Mr Rondon Turismo";
        //Distance in meters to get places nearby
        public const int GetPlacesUntil = 1000;
        public static readonly DefaultSetting DefaultSetting = new DefaultSetting();
    }

    public class DefaultSetting
    {
        public readonly City City = new City { CityId = 1, Name = "Porto Velho" };
    }
}