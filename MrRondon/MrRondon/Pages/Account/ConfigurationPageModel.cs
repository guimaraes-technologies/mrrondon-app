using System;
using System.Windows.Input;
using MrRondon.Auth;
using MrRondon.Helpers;
using Xamarin.Forms;

namespace MrRondon.Pages.Account
{
    public class ConfigurationPageModel : BasePageModel
    {
        public ConfigurationPageModel()
        {
            Title = "Configurações";
            var until = ApplicationManager<object>.Find("PlaceUntil");

            var value = until == null ? AccountManager.DefaultSetting.PlaceUntil : double.Parse(until.ToString());
            SetValue(value);
        }

        private string _placeDescription;
        public string PlaceDescription
        {
            get => _placeDescription;
            set => SetProperty(ref _placeDescription, value);
        }

        private double _placeUntil;
        public double PlaceUntil
        {
            get => _placeUntil;
            set => SetProperty(ref _placeUntil, value);
        }

        public void SetValue(double until)
        {
            PlaceUntil = Math.Round(until);
            PlaceDescription = $"{PlaceUntil} metros";
            ApplicationManager<object>.AddOrUpdate("PlaceUntil", until);
        }
    }
}