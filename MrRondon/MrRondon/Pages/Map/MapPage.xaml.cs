using MrRondon.Helpers;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Threading.Tasks;
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
                var permissionGranted = await CheckPermission();
                if (permissionGranted)
                {
                    var currentPosition = await GeolocatorHelper.GetCurrentPositionAsync();

                    _pageModel.LoadPinsCommand.Execute(currentPosition);

                    foreach (var item in _pageModel.Pins) Companies.Pins.Add(item);

                    Companies.MoveToRegion(MapSpan.FromCenterAndRadius(
                        new Position(currentPosition.Latitude, currentPosition.Longitude), Distance.FromKilometers(2)));
                }
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
            }
            finally
            {
                _pageModel.IsLoading = false;
                _pageModel.IsPresented = false;
            }
        }

        private async Task<bool> CheckPermission()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await _pageModel.MessageService.ShowAsync("Permissão necessária", "Por favor, precisamos desta permissão para exibir os locais no mapa");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    status = results[Permission.Location];
                }

                if (status == PermissionStatus.Granted) return true;
                else if (status != PermissionStatus.Unknown)
                {
                    await _pageModel.MessageService.ShowAsync("Permissão negada", "Não é possível continuar, pois a permissão não foi concedida.");
                }
                return false;
            }
            catch (Exception ex)
            {
                _pageModel.ExceptionService.TrackError(ex);
                await _pageModel.MessageService.ShowAsync(ex);
                return false;
            }
        }
    }
}