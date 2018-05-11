using MrRondon.Services.Interfaces;
using Xamarin.Forms;

namespace MrRondon.Pages.Account
{
    public class LogoutPage : ContentPage
    {
        protected INavigationService NavigationService;

        public LogoutPage()
        {
            NavigationService = DependencyService.Get<INavigationService>();
            NavigationService.NavigateOut();
        }
    }
}