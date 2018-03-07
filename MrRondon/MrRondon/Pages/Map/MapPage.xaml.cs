using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MrRondon.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MrRondon.Pages.Map
{
    public partial class MapPage : ContentPage
    {
        private readonly MapPageModel _pageModel;

        public MapPage()
        {
            InitializeComponent();
            BindingContext = _pageModel = _pageModel ?? new MapPageModel();
        }

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                var currentPosition = await GeolocatorHelper.GetCurrentPositionAsync();

                var mapSpan = MapSpan.FromCenterAndRadius(new Position(currentPosition.Latitude, currentPosition.Longitude), Distance.FromKilometers(2));

                foreach (var item in _pageModel.Pins) Companies.Pins.Add(item);
                _pageModel.LoadPinsCommand.Execute(currentPosition);
                Companies.MoveToRegion(mapSpan);
            }
            catch (TaskCanceledException ex)
            {
                Debug.WriteLine(ex);
                await _pageModel.MessageService.ShowAsync("Informação", "A requisição está demorando muito, verifique sua conexão com a internet.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await _pageModel.MessageService.ShowAsync(ex.Message);
            }
        }
    }
}