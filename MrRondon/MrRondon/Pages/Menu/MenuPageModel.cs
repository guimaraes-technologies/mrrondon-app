using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using MrRondon.Helpers;
using MrRondon.Pages.Account;
using MrRondon.Pages.Event;
using MrRondon.Services;
using MrRondon.ViewModels;
using Xamarin.Forms;

namespace MrRondon.Pages.Menu
{
    public class MenuPageModel : BasePageModel
    {
        private ObservableRangeCollection<MenuItemVm> _items;
        public ObservableRangeCollection<MenuItemVm> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        private string _siginSignoutText = "Entrar";
        public string SiginSignoutText
        {
            get => _siginSignoutText;
            set
            {
                if (_siginSignoutText == value) return;

                _siginSignoutText = value;
                SetProperty(ref _siginSignoutText, value);
            }
        }

        public ICommand LoadItemsCommand { get; set; }
        public ICommand AboutCommand { get; set; }
        public ICommand SiginSignoutCommand { get; set; }

        public MenuPageModel()
        {
            Title = "Bem vindo(a), Oziel Guimarães";
            LoadItemsCommand = new Command(async () => await ExecuteLoadItems());
            AboutCommand = new Command(async () => await ExecuteAbout());
            SiginSignoutCommand = new Command(async () => await ExecuteSigninSignout());
        }

        private async Task ExecuteLoadItems()
        {
            try
            {
                if (IsLoading) return;

                IsLoading = true;
                var items = await MenuHelper.Build();
                Items = new ObservableRangeCollection<MenuItemVm>(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await NavigationService.PushAsync(new ErrorPage(new ErrorPageModel(ex.Message, Title) { IsLoading = false }));
            }
            finally
            {
                IsLoading = false;
                IsPresented = false;
            }
        }

        private async Task ExecuteAbout()
        {
            await MessageService.ShowAsync($"O aplicativo {Constants.AppName}, foi desenvolvimento pela equipe GoNew, para a SETUR - Secretaria de Turismo.");
        }

        private async Task ExecuteSigninSignout()
        {
            var service = new UserService();
            if (Auth.Account.Current.IsLoggedIn)
            {
                service.Logout();

                await NavigationService.PushAsync(new MainPage());
                return;
            }

            await NavigationService.PushModalAsync(new LoginPage());
        }
    }
}