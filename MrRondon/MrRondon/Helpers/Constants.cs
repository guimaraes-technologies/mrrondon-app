using System;
using MrRondon.Entities;

namespace MrRondon.Helpers
{
    public static class Constants
    {
        public const string Host = "http://api.mrrondon.ozielguimaraes.net";
        public const string GoogleKey = "AIzaSyDHRHwsvyfA7CMzXAUkc50UILekWrxych4";

        // public static string Host => "http://localhost:1111";
        public const string AppName = "Mr Rondon Turismo";
        //Distance in meters to get places nearby
        public const int GetPlacesUntil = 1000;
        public static readonly DefaultSetting DefaultSetting = new DefaultSetting();
        public const string TokenType = "Bearer";
        public const string ClientId = "mrrondon.app";
        public const string ClientSecret = "Mr.Rondon.Turismo.App";
    }

    public class DefaultSetting
    {
        public readonly City City = new City { CityId = 1, Name = "Porto Velho" };
        public readonly string TelephoneSetur = "(69) 3216-1044";
        public readonly double Latitude = -8.7592547;
        public readonly double Longitude = -63.8769227;
    }
}