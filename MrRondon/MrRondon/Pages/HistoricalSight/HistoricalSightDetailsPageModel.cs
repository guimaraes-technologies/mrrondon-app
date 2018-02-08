using System.Threading.Tasks;
using System.Windows.Input;
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
            Title = model.Name;
            HistoricalSight = model;
            OpenMapCommand = new Command(OpenMap);
            MakeCallCommand = new Command(ExecuteMakeCall);
            MarkAsFavoriteCommand = new Command(async () => await MarkAsFavorite());
            ShareCommand = new Command(async () => await Share());
        }

        public ICommand MakeCallCommand { get; set; }
        public ICommand OpenMapCommand { get; set; }
        public ICommand ShareCommand { get; set; }
        public ICommand MarkAsFavoriteCommand { get; set; }

        private Entities.HistoricalSight _historicalSight;
        public Entities.HistoricalSight HistoricalSight
        {
            get => _historicalSight;
            set => SetProperty(ref _historicalSight, value);
        }

        private void ExecuteMakeCall()
        {
            NavigationService.NavigateToUrl(Constants.DefaultSetting.TelephoneSetur);
        }

        private void OpenMap()
        {
            CrossExternalMaps.Current.NavigateTo(HistoricalSight.Name, HistoricalSight.Address.Latitude, HistoricalSight.Address.Longitude, NavigationType.Driving);
        }

        private async Task MarkAsFavorite()
        {
            await MessageService.ToastAsync($"Ainda não implementado \n{HistoricalSight.HistoricalSightId}");
        }

        private async Task Share()
        {
            var message = new ShareMessage
            {
                Title = Constants.AppName,
                Text = $"Olha o que eu encontrei no {Constants.AppName}:\nPonto Turístico: {HistoricalSight.Name}\nLocal: {HistoricalSight.Address.FullAddressInline}\nMuito TOP, dá uma olhada ;)\n",
                Url = "https://play.google.com/store/apps/details?id=br.gov.ro.setur.mrrondon"
            };
            await CrossShare.Current.Share(message);
        }
    }
}