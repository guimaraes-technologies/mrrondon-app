using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MrRondon.Helpers;
using MrRondon.Services;
using Xamarin.Forms;

namespace MrRondon.Pages.Account
{
    public class LoginPageModel : BasePageModel
    {
        private Page _callBackPage;
        public Page CallBackPage
        {
            get => _callBackPage;
            private set => SetProperty(ref _callBackPage, value);
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        public LoginPageModel(Page callBack = null)
        {
            CallBackPage = callBack;
            Title = "Login";
            IsLoading = false;
            LoginCommand = new Command(async item => await ExecuteSignIn());
            RegisterCommand = new Command(ExecuteRegister);
        }

        public async Task ExecuteSignIn()
        {
            try
            {
                IsLoading = true;
                var userService = new UserService();
                var isAuthenticated = await userService.Authenticate(this);
                if (isAuthenticated)
                {
                    NavigationService.RemovePage(new LoginPage());
                    await NavigationService.PopModalAsync();
                    IsPresented = false;
                    if (CallBackPage != null)
                    {
                        await NavigationService.PushAsync(CallBackPage);
                    }
                    else
                    {
                        //Application.Current.MainPage = new MasterPage();
                        await NavigationService.PopToRootAsync();
                    }
                }
                else await MessageService.ShowAsync("Autenticação", "Usuário ou Senha incorreta");
            }
            catch (Exception ex)
            {
                await MessageService.ShowAsync("Autenticação", ex.Message);
            }
            finally
            {
                IsLoading = false;
                IsPresented = false;
            }
        }

        private void RemovePageFromStack()
        {
            var existingPage = NavigationService.GetNavigationStack();
            foreach (var page in existingPage)
            {
                if (page.GetType() == typeof(LoginPage)) NavigationService.RemovePage(page);
            }
        }

        private async void ExecuteRegister()
        {
            await NavigationService.PopModalAsync();
            await NavigationService.PushAsync(new MasterPage(new RegisterPage()));
        }
    }
}