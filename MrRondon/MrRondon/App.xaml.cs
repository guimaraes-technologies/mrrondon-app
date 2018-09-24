using System;
using System.Linq;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MrRondon.Helpers;
using MrRondon.Pages;
using MrRondon.Services;
using MrRondon.Services.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MrRondon
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            RefactorColorsToHexString();

            DependencyService.Register<IMessageService, MessageService>();
            DependencyService.Register<IExceptionService, ExceptionService>();
            DependencyService.Register<INavigationService, NavigationService>();
            Startup.Run();
            MainPage = new NavigationPage(new MasterPage());
        }

        private void RefactorColorsToHexString()
        {
            try
            {

                for (var i = 0; i < Resources.Count; i++)
                {
                    var key = Resources.Keys.ElementAt(i);
                    var resource = Resources[key];

                    if (resource is Color color) Resources.Add($"{key}HexString", color.ToHexString());
                }
            }
            catch (Exception ex)
            {
                var exception = DependencyService.Get<IExceptionService>();
                exception.TrackError(ex);
            }
        }

        protected override void OnStart()
        {
            AppCenter.Start($"android={Constants.AppCenter.Android};ios={Constants.AppCenter.iOS}",
                typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
