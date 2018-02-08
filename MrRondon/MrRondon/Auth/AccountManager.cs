using System.Linq;
using System.Threading.Tasks;
using MrRondon.Entities;
using MrRondon.Helpers;
using MrRondon.Services;
using Xamarin.Forms;

namespace MrRondon.Auth
{
    public class AccountManager
    {
        public AccountManager()
        {
            SetUser();
            //SetCity();
        }

        public User User { get; private set; }
        public bool IsLoggedIn { get; private set; }

        public City City => ApplicationManager<City>.Find("city");

        private void SetUser()
        {
            if (Application.Current.Properties.ContainsKey("user"))
            {
                User = Application.Current.Properties["user"] as User;
                IsLoggedIn = true;
            }
            else IsLoggedIn = false;
        }
    }
}