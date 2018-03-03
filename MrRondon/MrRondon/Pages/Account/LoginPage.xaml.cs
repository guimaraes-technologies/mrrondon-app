using Xamarin.Forms;

namespace MrRondon.Pages.Account
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginPageModel();
        }
    }
}