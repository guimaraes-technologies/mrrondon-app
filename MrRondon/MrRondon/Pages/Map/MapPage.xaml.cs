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
            if (Device.RuntimePlatform.Equals(Device.iOS)) Icon = "ic_map";
            BindingContext = _pageModel = _pageModel ?? new MapPageModel();


            //var customMap = new MapExtension
            //{
            //    IsShowingUser = true,
            //    HeightRequest = 400,
            //    MapType = MapType.Street,
            //    HorizontalOptions = LayoutOptions.FillAndExpand
            //};
            //parentStack.Children.Add(customMap);
            //var pageModel = new MapPageModel();
            //pageModel.GetCurrentPositionCommand.Execute(null);
            //pageModel.LoadPinsCommand.Execute(pageModel.CurrentPosition);
            //customMap.Items = pageModel.Pins;
        }

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                var currentPosition = await GeolocatorHelper.GetCurrentPositionAsync();

                _pageModel.LoadPinsCommand.Execute(currentPosition);

                foreach (var item in _pageModel.Pins) Companies.Pins.Add(item);

                Companies.MoveToRegion(MapSpan.FromCenterAndRadius(
                    new Position(currentPosition.Latitude, currentPosition.Longitude), Distance.FromKilometers(2)));
            }
            catch (TaskCanceledException ex)
            {
                _pageModel.ExceptionService.TrackError(ex);
                await _pageModel.MessageService.ShowAsync("Informação",
                    "A requisição está demorando muito, verifique sua conexão com a internet.");
            }
            catch (Exception ex)
            {
                _pageModel.ExceptionService.TrackError(ex);
                await _pageModel.MessageService.ShowAsync(ex);
            }
            finally
            {
                _pageModel.IsLoading = false;
                _pageModel.IsPresented = false;
            }
        }
    }
}