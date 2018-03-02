using System.Threading.Tasks;
using System.Windows.Input;
using MrRondon.Auth;
using MrRondon.Helpers;
using Plugin.ExternalMaps;
using Plugin.ExternalMaps.Abstractions;
using Plugin.Share;
using Plugin.Share.Abstractions;
using Xamarin.Forms;

namespace MrRondon.Pages.HistoricalSight
{
    public class HistoricalSightDetailsPageModel : BasePageModel
    {
        public HistoricalSightDetailsPageModel(Entities.HistoricalSight model)
        {
            Title = "Detalhes do Patrimônio Histórico";
            HistoricalSight = model;
            OpenMapCommand = new Command(OpenMap);
            MakePhoneCallCommand = new Command(ExecuteMakeCall);
            ShareCommand = new Command(async () => await Share());
        }

        public ICommand MakePhoneCallCommand { get; set; }
        public ICommand OpenMapCommand { get; set; }
        public ICommand ShareCommand { get; set; }

        private string _favoriteIcon;
        public string FavoritIcon
        {
            get => _favoriteIcon;
            set => SetProperty(ref _favoriteIcon, value);
        }

        private Entities.HistoricalSight _historicalSight;
        public Entities.HistoricalSight HistoricalSight
        {
            get => _historicalSight;
            set => SetProperty(ref _historicalSight, value);
        }

        private void ExecuteMakeCall()
        {
            NavigationService.NavigateToUrl(AccountManager.DefaultSetting.TelephoneSetur);
        }

        private void OpenMap()
        {
            CrossExternalMaps.Current.NavigateTo(HistoricalSight.Name, HistoricalSight.Address.Latitude, HistoricalSight.Address.Longitude, NavigationType.Driving);
        }

        private async Task Share()
        {
            var message = new ShareMessage
            {
                Title = Constants.AppName,
                Text = $"Olha o que eu encontrei no {Constants.AppName}:\nPonto Turístico: {HistoricalSight.Name}\nLocal: {HistoricalSight.Address.FullAddressInline}\nMuito TOP, dá uma olhada ;)\n",
                Url = "http://mrrondon.ozielguimaraes.net"
            };
            await CrossShare.Current.Share(message);
        }
    }
}