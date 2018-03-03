using System;
using Xamarin.Forms;

namespace MrRondon.Pages.Account
{
    //http://www.c-sharpcorner.com/article/xamarin-forms-create-a-login-page-mvvm/
    public partial class LoginPage : ContentPage
    {
        private Action<bool> OnAuthenticate;

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginPageModel();
            //OnAuthenticate = authenticate;
        }
    }
}