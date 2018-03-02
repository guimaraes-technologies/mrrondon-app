using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MrRondon.Pages.Account;
using MrRondon.Pages.Event;
using MrRondon.ViewModels;
using Xamarin.Forms;

namespace MrRondon.Pages.Menu
{
    public class MenuHelper
    {
        public static Page GetPage(MenuItemVm item)
        {
            var account = Auth.Account.Current;

            switch (item.Type)
            {
                case MenuType.Home: return new MainPage();
                case MenuType.AccountInformation:
                    if (!account.IsTokenExpired && account.IsLoggedIn)
                        return new InformationPage(new InformationPageModel(account.User));
                    return new LoginPage();
                case MenuType.Configurations: return new ConfigurationPage();
                case MenuType.FavoriteEvent:
                    if (!account.IsTokenExpired && account.IsLoggedIn) return new FavoriteEventsPage();
                    return new LoginPage();
                default: return new MainPage();
            }
        }

        public static async Task<IEnumerable<MenuItemVm>> Build()
        {
            var items = new List<MenuItemVm>
            {
                new MenuItemVm("Início", "home", MenuType.Home)
            };

            var account = Auth.Account.Current;
            if (account.IsLoggedIn && !account.IsTokenExpired)
            {
                items.Add(new MenuItemVm("Eventos Favoritos", "favorite", MenuType.FavoriteEvent));
                items.Add(new MenuItemVm("Minha Conta", "user", MenuType.AccountInformation));
            }

            items.Add(new MenuItemVm("Configurações", "configuration", MenuType.Configurations));
            await Task.Delay(1);

            return items;
        }
    }
}