using Xamarin.Forms;

namespace MrRondon.Pages.Account
{
    public partial class ConfigurationPage : ContentPage
    {
        public ConfigurationPage()
        {
            InitializeComponent();
            BindingContext = new ConfigurationPageModel();
        }
    }
}