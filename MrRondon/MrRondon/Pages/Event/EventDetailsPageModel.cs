using System.Threading.Tasks;
using System.Windows.Input;
using MrRondon.Helpers;
using MrRondon.Services;
using Plugin.ExternalMaps;
using Plugin.ExternalMaps.Abstractions;
using Plugin.Share;
using Plugin.Share.Abstractions;
using Xamarin.Forms;

namespace MrRondon.Pages.Event
{
    public class EventDetailsPageModel : BasePageModel
    {
        public EventDetailsPageModel(Entities.Event model)
        {
            Title = model.Name;
            Event = model;
            MakePhoneCallCommand = new Command(MakePhoneCall);
            OpenMapCommand = new Command(OpenMap);
            MarkAsFavoriteCommand = new Command(async () => await MarkAsFavorite());
            ShareCommand = new Command(async () => await Share());
            SetFavoritIcon();
        }

        public ICommand MakePhoneCallCommand { get; set; }
        public ICommand OpenMapCommand { get; set; }
        public ICommand ShareCommand { get; set; }
        public ICommand MarkAsFavoriteCommand { get; set; }

        private Entities.Event _event;
        public Entities.Event Event
        {
            get => _event;
            set => SetProperty(ref _event, value);
        }

        public string FavoritIcon { get; }
        
        private void MakePhoneCall()
        {
            NavigationService.MakePhoneCall("993212372");
        }

        private void OpenMap()
        {
            CrossExternalMaps.Current.NavigateTo(Event.Name, Event.Address.Latitude, Event.Address.Longitude, NavigationType.Driving);
        }

        private async Task MarkAsFavorite()
        {
            await MessageService.ToastAsync($"Ainda não implementado \n{Event.EventId}");
        }

        private async Task Share()
        {
            var rangeDate = Event.StartDate.Date == Event.EndDate.Date
                ? Event.StartDate.ToShortDateString()
                : $"{Event.StartDate.ToShortDateString()} até {Event.EndDate.ToShortDateString()}";
            var message = new ShareMessage
            {
                Title = Constants.AppName,
                Text = $"Olha o que eu encontrei no {Constants.AppName}:\nEvento: {Event.Name}\nData: {rangeDate}\nLocal: {Event.Address.FullAddressInline}\nMuito TOP, dá uma olhada ;)\n",
                Url = "https://play.google.com/store/apps/details?id=br.gov.ro.setur.mrrondon"
            };
            await CrossShare.Current.Share(message);
        }

        private async Task SetFavoritIcon()
        {
            var service = new FavoriteEventService();
            var x = await service.GetAsync();

        }
    }
}