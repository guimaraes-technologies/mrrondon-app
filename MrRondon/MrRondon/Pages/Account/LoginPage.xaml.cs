using Xamarin.Forms;

namespace MrRondon.Pages.Account
{
    //http://www.c-sharpcorner.com/article/xamarin-forms-create-a-login-page-mvvm/
    public partial class LoginPage : ContentPage
    {
        public LoginPage(Page callBack = null)
        {
            InitializeComponent();
            BindingContext = new LoginPageModel(callBack);
        }
    }
}