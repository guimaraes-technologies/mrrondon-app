using MrRondon.Helpers;
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

            DependencyService.Register<IMessageService, MessageService>();
            DependencyService.Register<INavigationService, NavigationService>();
            MainPage = new NavigationPage(new Pages.MainPage()) { Title = Constants.AppName };

            Startup.Run();
            //MainPage = new Pages.MainPage();
        }
        //todo Diminuir o tamanho dos icones de detalhe evento
        //todo nem todas categoria possui SUB categoria

        protected override void OnStart()
        {
            // Handle when your app starts
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
