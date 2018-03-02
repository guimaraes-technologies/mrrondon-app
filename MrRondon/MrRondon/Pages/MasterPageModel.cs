using System.Threading.Tasks;
using System.Windows.Input;
using MrRondon.Helpers;
using MrRondon.Pages.Account;
using MrRondon.Services;
using Xamarin.Forms;

namespace MrRondon.Pages
{
    public class MasterPageModel : BasePageModel
    {
        public MasterPageModel()
        {
            Title = Constants.AppName;
            AboutCommand = new Command(async () => await ExecuteAbout());
            SiginSignoutCommand = new Command(async () => await ExecuteSigninSignout());
        }

        public ICommand AboutCommand { get; set; }
        public ICommand SiginSignoutCommand { get; set; }

        private string _siginSignoutText;
        public string SiginSignoutText
        {
            get => _siginSignoutText;
            set
            {
                if (_siginSignoutText == value) return;

                 _siginSignoutText=value;
            }
        }

        private async Task ExecuteAbout()
        {
            await MessageService.ShowAsync($"O aplicativo {Constants.AppName}, foi desenvolvimento pela equipe GoNew, para a SETUR - Secretaria de Turismo.");
        }

        private async Task ExecuteSigninSignout()
        {
            var service = new UserService();
            if (Auth.Account.Current.IsLoggedIn)
            {
                service.Logout();

                await NavigationService.PushAsync(new MainPage());
                return;
            }

            await NavigationService.PushModalAsync(new LoginPage());
        }
    }
}