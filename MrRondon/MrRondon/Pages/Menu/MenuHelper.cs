using System.Collections.Generic;
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
                case MenuType.AccountInformation:
                    if (account.IsValid) return new InformationPage(new InformationPageModel(account.User));
                    return new LoginPage();
                case MenuType.Configurations: return new ConfigurationPage();
                case MenuType.FavoriteEvent:
                    if (account.IsValid) return new FavoriteEventsPage();
                    return new LoginPage();
                case MenuType.ContactUs: return new ContactUsPage();
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
                items.Add(new MenuItemVm("Eventos Favoritos", "favorite", MenuType.FavoriteEvent));
            }
            items.Add(new MenuItemVm("Configurações", "configuration", MenuType.Configurations));

            if(account.IsValid)items.Add(new MenuItemVm("Sair", "logout", MenuType.Logout));
            else items.Add(new MenuItemVm("Entrar", string.Empty, MenuType.Logout));

            await Task.Delay(1);
            return items;
        }
    }
}