using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MrRondon.Auth;
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
            Title = "Detalhes do Evento";
            Event = model;
            IsFavorit = model.IsFavorite;
            MakePhoneCallCommand = new Command(MakePhoneCall);
            OpenMapCommand = new Command(OpenMap);
            MarkAsFavoriteCommand = new Command(async () => await ExecuteFavorite());
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

        private string _favoritIcon;
        public string FavoritIcon
        {
            get => _favoritIcon;
            set => SetProperty(ref _favoritIcon, value);
        }

        private bool _isFavorit;
        public bool IsFavorit
        {
            get => _isFavorit;
            set
            {
                FavoritIcon = _isFavorit ? "favorite" : "unfavorite";
                if (_isFavorit == value) return;

                _isFavorit = value;
                Notify(nameof(IsFavorit));
                FavoritIcon = _isFavorit ? "favorite" : "unfavorite";
            }
        }

        private void MakePhoneCall()
        {
            NavigationService.MakePhoneCall(AccountManager.DefaultSetting.TelephoneSetur);
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
                IsFavorit = !IsFavorit;
                var isFavorite = await service.FavoriteAsync(Event.EventId);
                IsFavorit = isFavorite;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                IsFavorit = false;
                await MessageService.ShowAsync(ex.Message);
            }
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
                Url = "http://mrrondon.ozielguimaraes.net"
            };
            await CrossShare.Current.Share(message);
        }

        private async Task SetFavoritIcon()
        {
            try
            {
                var service = new FavoriteEventService();
                var isFavorite = await service.IsFavorite(Event.EventId);
                IsFavorit = isFavorite;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}