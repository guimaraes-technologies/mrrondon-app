﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MrRondon.Auth;
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
                case MenuType.Configurations: return new ConfigurationPage();
                case MenuType.FavoriteEvent:
                    if (account.IsValid) return new FavoriteEventsPage();
                    return new LoginPage();
                case MenuType.ContactUs: return new ContactUsPage();
                case MenuType.Logout: return new LogoutPage();
                case MenuType.Login: return new LoginPage();
                default: return new MainPage();
            }
        }

        public static async Task<IEnumerable<MenuItemVm>> Build(AccountManager account)
        {
            var items = new List<MenuItemVm>
            {
                new MenuItemVm("Início", "home", MenuType.Home),
                new MenuItemVm("Fale com a gente", "contact", MenuType.ContactUs)
            };

            if (account.IsValid)
            {
                items.Add(new MenuItemVm("Favoritos", "favorite", MenuType.FavoriteEvent));
            }
            items.Add(new MenuItemVm("Configurações", "configuration", MenuType.Configurations));

            items.Add(account.IsValid ? new MenuItemVm("Sair", "logout", MenuType.Logout) : new MenuItemVm("Entrar", "logout", MenuType.Login));

            await Task.Delay(1);
            return items;
        }
    }
}