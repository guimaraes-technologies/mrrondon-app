using MrRondon.Helpers;
using MrRondon.Services;
using Plugin.ExternalMaps;
using Plugin.ExternalMaps.Abstractions;
using Plugin.Share;
using Plugin.Share.Abstractions;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MrRondon.Pages.Event
{
    public class EventDetailsPageModel : BasePageModel
    {
        public EventDetailsPageModel(Entities.Event model)
        {
            Title = "Detalhes do Evento";
            Event = model;
            IsFavorite = model.IsFavorite;
            MakePhoneCallCommand = new Command(MakePhoneCall);
            OpenMapCommand = new Command(OpenMap);
            MarkAsFavoriteCommand = new Command(async () => await ExecuteFavorite());
            ShareCommand = new Command(async () => await ExecuteShare());
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

        private string _favoritIcon;
        public string FavoritIcon
        {
            get => _favoritIcon;
            set => SetProperty(ref _favoritIcon, value);
        }

        private bool _isFavorite;
        public bool IsFavorite
        {
            get => _isFavorite;
            set
            {
                FavoritIcon = _isFavorite ? "favorite" : "unfavorite";
                if (_isFavorite == value) return;

                _isFavorite = value;
                Notify(nameof(IsFavorite));
                FavoritIcon = _isFavorite ? "favorite" : "unfavorite";
            }
        }

        private void MakePhoneCall()
        {
            NavigationService.MakePhoneCall(Constants.TelephoneSetur);
        }

        private void OpenMap()
        {
            CrossExternalMaps.Current.NavigateTo(Event.Name, Event.Address.Latitude, Event.Address.Longitude, NavigationType.Driving);
        }

        private async Task ExecuteFavorite()
        {
            try
            {
                var service = new FavoriteEventService();
                IsFavorite = !IsFavorite;
                var isFavorite = await service.FavoriteAsync(Event.EventId);
                IsFavorite = isFavorite;
            }
            catch (TaskCanceledException ex)
            {
                ExceptionService.TrackError(ex);
                await MessageService.ShowAsync("Informação", "A requisição está demorando muito, verifique sua conexão com a internet.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                IsFavorite = false;
                await MessageService.ShowAsync(ex.Message);
            }
        }

        private async Task ExecuteShare()
        {
            var service = new EventService();
            var eventInformation = await service.GetAsync(Event.EventId);
            var rangeDate = eventInformation.StartDate.Date == eventInformation.EndDate.Date
                ? eventInformation.StartDate.ToShortDateString()
                : $"{eventInformation.StartDate.ToShortDateString()} até {eventInformation.EndDate.ToShortDateString()}";
            var message = new ShareMessage
            {
                Title = Constants.AppName,
                Text = $"Olha o que eu encontrei no {Constants.AppName}:\nEvento: {eventInformation.Name}\nData: {rangeDate}\nLocal: {eventInformation.Address.FullAddressInline}\nMuito TOP, dá uma olhada ;)\n",
                Url = Constants.SystemUrl
            };
            await CrossShare.Current.Share(message);
        }
    }
}