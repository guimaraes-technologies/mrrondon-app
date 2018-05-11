using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using MrRondon.Helpers;
using MrRondon.Pages.Account;
using MrRondon.Pages.Event;
using MrRondon.Services;
using MrRondon.Services.Interfaces;
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

        public string AppVersion { get; set; }

        private string _siginSignoutText = "Entrar";
        public string SiginSignoutText
        {
            get => _siginSignoutText;
            set => SetProperty(ref _siginSignoutText, value);
        }

        public ICommand LoadItemsCommand { get; set; }
        public ICommand AboutCommand { get; set; }
        public ICommand VersionCommand { get; set; }
        public ICommand SiginSignoutCommand { get; set; }

        public MenuPageModel()
        {
            LoadItemsCommand = new Command(async () => await ExecuteLoadItems());
            AboutCommand = new Command(ExecuteAbout);
            VersionCommand = new Command(async () => await ExecuteVersion());
            SiginSignoutCommand = new Command(async () => await ExecuteSigninSignout());
            AppVersion = $"Versão {DependencyService.Get<IAppVersion>().GetVersion()}";
        }

        private async Task ExecuteLoadItems()
        {
            try
            {
                if (IsLoading) return;
                IsLoading = true;

                var account = Auth.Account.Current;
                var items = await MenuHelper.Build(account);
                if (account.IsValid)
                {
                    SiginSignoutText = "Sair";
                    MenuTitle = account.User.FullName;
                }
                else
                {
                    SiginSignoutText = "Entrar";
                    MenuTitle = "Visitante";
                }

                Items = new ObservableRangeCollection<MenuItemVm>(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await MessageService.ShowAsync(ex.Message);
            }
            finally
            {
                IsLoading = false;
                IsPresented = false;
            }
        }

        private void ExecuteAbout()
        {
            NavigationService.NavigateToUrl(Constants.SystemUrl);
        }

        private async Task ExecuteVersion()
        {
            var wannaToContactDeveloper = await MessageService.ShowConfirmationAsync($"O aplicativo {Constants.AppName}, foi desenvolvido pela equipe GoNew(Marcel Rios, Mirian Rios e Oziel Guimarães), para a SETUR - Superintendência Estadual do Turismo no desafio da HACKATHON 2017.\nDeseja entrar em contato com o desenvolvedor?","Sim", "Agora não");
            if (wannaToContactDeveloper) NavigationService.NavigateToUrl(Constants.DeveloperUrl);
        }

        private async Task ExecuteSigninSignout()
        {
            try
            {
                var service = new UserService();
                var account = Auth.Account.Current;
                if (account.IsValid)
                {
                    await NavigationService.PushAsync(new MasterPage());
                    return;
                }

                service.Logout();
                await NavigationService.PushModalAsync(new LoginPage());
            }
            finally
            {
                IsLoading = false;
                IsPresented = false;
            }
        }
    }
}