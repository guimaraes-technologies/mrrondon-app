using MrRondon.Helpers;
using MrRondon.Pages.Category;
using MrRondon.Pages.Event;
using MrRondon.Pages.Map;
using MrRondon.Services.Interfaces;
using System.Threading.Tasks;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace MrRondon.Pages
{
    public partial class MainPage : TabbedPage
    {
        private readonly ListEventPage _listEventPage;

        public MainPage()
        {
            InitializeComponent();
            Title = Constants.AppName;

            Children.Add(new ListCategoriesPage());
            Children.Add(_listEventPage = new ListEventPage());

            if (Device.RuntimePlatform == Device.Android)
            {
                var androidSettings = DependencyService.Get<IAndroidSettings>();
                if (androidSettings.GetAndroidApiLevel() >= 21)
                {
                    var page = _listEventPage.BindingContext as ListEventPageModel;
                    var hasPermission = Task.Run(() => page.HasPermissionAsync(Permission.Location, Permission.LocationWhenInUse));

                    if (hasPermission.Result) Children.Add(new MapPage());
                    else Children.Add(new PermissionDeniedPage("MAPA", "Localização"));
                }
                else Children.Add(new MapPage());
            }
            else Children.Add(new MapPage());
        }
    }
}