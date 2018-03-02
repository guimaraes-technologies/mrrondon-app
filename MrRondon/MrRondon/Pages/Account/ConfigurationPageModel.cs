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

            PlaceUntil = until == null ? AccountManager.DefaultSetting.PlaceUntil : int.Parse(until.ToString());
            UpdateCommand = new Command<int>(ExecuteUpdate);
        }
        public ICommand UpdateCommand { get; set; }

        private int _placeUntil;
        public int PlaceUntil
        {
            get => _placeUntil;
            set => SetProperty(ref _placeUntil, value);
        }

        private void ExecuteUpdate(int until)
        {
            PlaceUntil = until;
            ApplicationManager<object>.AddOrUpdate("PlaceUntil", until);
        }
    }
}