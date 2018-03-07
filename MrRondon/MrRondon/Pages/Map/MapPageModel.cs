using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using MrRondon.Exceptions;
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
            ChangeActualCityCommand = new Command(async () => await ExecuteChangeActualCity(new MapPage()));
        }

        public ICommand LoadPinsCommand { get; set; }
        public ICommand SetActualCityCommand { get; set; }

        private ObservableRangeCollection<Pin> _pins = new ObservableRangeCollection<Pin>();
        public ObservableRangeCollection<Pin> Pins
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

                foreach (var item in places)
                {
                    var pin = new Pin
                    {
                        Id = item.Id,
                        Label = item.Label,
                        Address = item.Address,
                        Type = PinType.Place,
                        Position = new Position(item.Position.Latitude, item.Position.Longitude)
                    };

                    Pins.Add(pin);
                }
            }
            catch (CouldNotGetLocationException ex)
            {
                Debug.WriteLine(ex);
                await MessageService.ShowAsync(ex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await MessageService.ShowAsync(ex.Message);
            }
            finally
            {
                IsLoading = false;
                IsPresented = false;
            }
        }
    }
}