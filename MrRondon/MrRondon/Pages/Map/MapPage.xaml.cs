using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using MrRondon.Extensions;
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

            var customMap = new MapExtension
            {
                IsShowingUser = true,
                HeightRequest = 400,
                MapType = MapType.Street,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            parentStack.Children.Add(customMap);
            var pageModel = new MapPageModel();
            pageModel.GetCurrentPositionCommand.Execute(null);
            pageModel.LoadPinsCommand.Execute(pageModel.CurrentPosition);
            customMap.Items = pageModel.Pins;

            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(pageModel.CurrentPosition.Latitude, pageModel.CurrentPosition.Longitude), Distance.FromMiles(1.0)));
        }

        //protected override async void OnAppearing()
        //{
        //    try
        //    {
        //        var currentPosition = await GeolocatorHelper.GetCurrentPositionAsync();

        //        var mapSpan = MapSpan.FromCenterAndRadius(new Position(currentPosition.Latitude, currentPosition.Longitude), Distance.FromKilometers(2));

        //        _pageModel.LoadPinsCommand.Execute(currentPosition);

        //        foreach (var item in _pageModel.Pins) Companies.Items.Add(item);

        //        Companies.MoveToRegion(mapSpan);

        //        base.OnAppearing();
        //    }
        //    catch (TaskCanceledException ex)
        //    {
        //        Debug.WriteLine(ex);
        //        await _pageModel.MessageService.ShowAsync("Informação", "A requisição está demorando muito, verifique sua conexão com a internet.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //        await _pageModel.MessageService.ShowAsync(ex.Message);
        //    }
        //}
    }
}