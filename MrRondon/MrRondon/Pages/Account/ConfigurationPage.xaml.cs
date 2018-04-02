using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace MrRondon.Pages.Account
{
    public partial class ConfigurationPage : ContentPage
    {
        private readonly ConfigurationPageModel _pageModel;

        public ConfigurationPage()
        {
            InitializeComponent();
            BindingContext = _pageModel = new ConfigurationPageModel();
            //TODO Avaliar e implementar
            //On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }
    }
}