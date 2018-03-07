using System;
using System.Diagnostics;
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
                if (_pageModel.Pins.Count == 0) _pageModel.LoadPinsCommand.Execute(currentPosition);

                var mapSpan = MapSpan.FromCenterAndRadius(new Position(currentPosition.Latitude, currentPosition.Longitude), Distance.FromKilometers(1));
                Companies.MoveToRegion(mapSpan);

                foreach (var item in _pageModel.Pins) Companies.Pins.Add(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await _pageModel.MessageService.ShowAsync(ex.Message);
            }
        }
    }
}