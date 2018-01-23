using System;
using System.Linq;
using MrRondon.Helpers;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace MrRondon.Pages.Map
{
    public partial class MapPage : ContentPage
    {
        private readonly MapPageModel _pageModel;

        public MapPage()
        {
            InitializeComponent();
            if (BindingContext == null) BindingContext = _pageModel = _pageModel ?? new MapPageModel();
            else _pageModel = (MapPageModel)BindingContext;
        }

        protected override async void OnAppearing()
        {
            if (_pageModel.Pins.Count == 0) _pageModel.LoadPinsCommand.Execute(null);
            var position =  await GeolocatorHelper.GetCurrentPositionAsync();
            Companies.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromMiles(1)));

            foreach (var item in _pageModel.Pins) Companies.Pins.Add(item);
            //base.OnAppearing();
        }
    }
}