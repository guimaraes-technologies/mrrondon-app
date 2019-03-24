using MrRondon.Helpers;
using MrRondon.Pages.Category;
using MrRondon.Pages.Event;
using MrRondon.Pages.Map;
using MrRondon.Services.Interfaces;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MrRondon.Pages
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            Title = Constants.AppName;

            Children.Add(new ListCategoriesPage());
            Children.Add(new ListEventPage());

            if (Device.RuntimePlatform == Device.Android)
            {
                var hasPermission = Task.Run(() => HasPermissionAsync(Permission.Location, Permission.LocationWhenInUse));
                if (hasPermission.Result) Children.Add(new MapPage());
                else Children.Add(new PermissionDeniedPage("MAPA", "Localização"));
            }
            else Children.Add(new MapPage());
        }

        public async Task<bool> HasPermissionAsync(params Permission[] permissions)
        {
            try
            {
                foreach (var item in permissions)
                {
                    var status = await CrossPermissions.Current.CheckPermissionStatusAsync(item);
                    if (status != PermissionStatus.Granted) return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                var exception = DependencyService.Get<IExceptionService>();
                exception.TrackError(ex, $"(HasPermissionAsync) {permissions}");
                return false;
            }
        }
    }
}