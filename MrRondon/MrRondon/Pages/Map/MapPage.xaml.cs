﻿using MrRondon.Helpers;
using MrRondon.Services;
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
            if (BindingContext == null) BindingContext = _pageModel = _pageModel ?? new MapPageModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (_pageModel.Pins.Count == 0) _pageModel.LoadPinsCommand.Execute(null);
            var position = await GeolocatorHelper.GetCurrentPositionAsync();

            var cityService = new CityService();
            var cityName = await cityService.GetCityName(position.Latitude, position.Longitude);
            _pageModel.SetActualCityCommand.Execute(cityName);

            var mapSpan = MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromMiles(1));
            Companies.MoveToRegion(mapSpan);

            foreach (var item in _pageModel.Pins) Companies.Pins.Add(item);
        }
    }
}