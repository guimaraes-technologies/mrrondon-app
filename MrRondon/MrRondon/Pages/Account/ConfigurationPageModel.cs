using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MrRondon.Auth;
using MrRondon.Entities;
using MrRondon.Extensions;
using MrRondon.Helpers;
using Xamarin.Forms;

namespace MrRondon.Pages.Account
{
    public class ConfigurationPageModel : BasePageModel
    {
        public ConfigurationPageModel()
        {
            Title = "Configurações";

            var precision = AccountManager.GetPrecision();
            SetValue(precision);
            Account = Auth.Account.Current;
        }

        public string MapRange { get; private set; }
        public AccountManager Account { get; }
        public bool NotHasContacts => !Account.User?.Contacts?.Any() ?? true;

        public void SetValue(int distance)
        {
            var unity = distance < 1000 ? "metros" : distance < 2000 ? "quilômetro" : "quilômetros";
            MapRange = $"{distance} {unity}";
            ApplicationManager<object>.AddOrUpdate("PlaceUntil", distance);
        }
    }
}