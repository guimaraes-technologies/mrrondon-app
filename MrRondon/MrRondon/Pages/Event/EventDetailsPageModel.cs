using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.ExternalMaps;
using Plugin.ExternalMaps.Abstractions;
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
            await MessageService.ToastAsync($"Ainda não implementado \n{Event.EventId}");
        }
    }
}