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
                if (_pageModel.Pins.Count == 0) _pageModel.LoadPinsCommand.Execute(null);
                var position = await GeolocatorHelper.GetCurrentPositionAsync();

                var mapSpan = MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromMiles(1));
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