using System;
using System.Linq;
using MrRondon.Auth;
using MrRondon.Helpers;

namespace MrRondon.Pages.Account
{
    public class ConfigurationPageModel : BasePageModel
    {
        public ConfigurationPageModel()
        {
            Title = "Configurações";

            var precision = AccountManager.GetPrecision();
            MapRange = precision;
            Account = Auth.Account.Current;
            Minimum = 0.100d;
            Maximum = 10000d;
        }

        private double _mapRange;
        public double MapRange
        {
            get => _mapRange;
            set
            {
                SetProperty(ref _mapRangeDescription, nameof(MapRange));
                SetValue(value);
            }
        }

        private string _mapRangeDescription;
        public string MapRangeDescription
        {
            get => _mapRangeDescription;
            set => SetProperty(ref _mapRangeDescription, value);
        }

        public double Minimum { get; set; }
        public double Maximum { get; set; }

        public AccountManager Account { get; }
        public bool NotHasContacts => !Account.User?.Contacts?.Any() ?? true;

        public void SetValue(double distance)
        {
            _mapRange = distance;
            var unity = distance < 1000 ? distance > 1 ? "metros" : "metro" : distance < 2000 ? "quilômetro" : "quilômetros";
            var value = Math.Round(distance / 1000);
            MapRangeDescription = $"{(distance < 1000 ? $"{Math.Round(distance)}" : $"{value}")} {unity}";

            ApplicationManager<object>.AddOrUpdate("PlaceUntil", distance);
        }
    }
}