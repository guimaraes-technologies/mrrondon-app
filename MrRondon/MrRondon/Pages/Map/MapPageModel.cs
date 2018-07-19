using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MrRondon.Exceptions;
using MrRondon.Extensions;
using MrRondon.Helpers;
using MrRondon.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MrRondon.Pages.Map
{
    public class MapPageModel : BasePageModel
    {
        public MapPageModel()
        {
            Title = Constants.AppName;
            LoadPinsCommand = new Command<Plugin.Geolocator.Abstractions.Position>(async (currentPosition) => await ExecuteLoadPins(currentPosition));
            GetCurrentPositionCommand = new Command(async () => await ExecuteGetCurrentPosition());
        }

        public ICommand LoadPinsCommand { get; set; }
        public ICommand GetCurrentPositionCommand { get; set; }

        public Plugin.Geolocator.Abstractions.Position CurrentPosition { get; set; }

        private ObservableRangeCollection<PinExtension> _pins = new ObservableRangeCollection<PinExtension>();
        public ObservableRangeCollection<PinExtension> Pins
        {
            get => _pins;
            set => SetProperty(ref _pins, value);
        }

        private async Task ExecuteLoadPins(Plugin.Geolocator.Abstractions.Position currentPosition)
        {
            try
            {
                if (IsLoading) return;
                IsLoading = true;

                var service = new LocationService();
                var places = await service.NearbyAsync(currentPosition);

                Pins.AddRange(places.Select(item =>
                    new PinExtension
                    {
                        Id = item.Id,
                        Label = item.Label,
                        Address = item.Address,
                        Type = PinType.Place,
                        Position = new Position(item.Position.Latitude, item.Position.Longitude),
                    }));
            }
            catch (CouldNotGetLocationException ex)
            {
                ExceptionService.TrackError(ex);
                await MessageService.ShowAsync(ex);
            }
            catch (Exception ex)
            {
                ExceptionService.TrackError(ex);
                await MessageService.ShowAsync(ex);
            }
            finally
            {
                IsLoading = false;
                IsPresented = false;
            }
        }

        private async Task ExecuteGetCurrentPosition()
        {
            CurrentPosition = await GeolocatorHelper.GetCurrentPositionAsync();
        }
    }
}