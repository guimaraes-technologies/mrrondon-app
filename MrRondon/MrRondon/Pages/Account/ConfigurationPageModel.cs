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

            _mapRange = AccountManager.GetPrecision();
            Account = Auth.Account.Current;
        }

        private double _mapRange;
        public double MapRange
        {
            get => _mapRange;
            set
            {
                var val = Math.Round(value);
                _mapRange = val;
                var unity = val < 1000 ? val > 1 ? "metros" : "metro" : val < 2000 ? "quilômetro" : "quilômetros";
                MapRangeDescription = $"{(val < 1000 ? $"{val}" : $"{Math.Round(val / 1000)}")} {unity}";

                ApplicationManager<object>.AddOrUpdate("PlaceUntil", val);
            }
        }

        private string _mapRangeDescription;
        public string MapRangeDescription
        {
            get => _mapRangeDescription;
            set => SetProperty(ref _mapRangeDescription, value);
        }

        public AccountManager Account { get; }
        public bool NotHasContacts => !Account.User?.Contacts?.Any() ?? true;
    }
}