using MrRondon.Entities;
using MrRondon.Services;

namespace MrRondon.Auth
{
    public class AccountManager
    {
        public AccountManager()
        {
            SetUser();
        }

        public User User { get; private set; }
        public bool IsLoggedIn { get; private set; }

        private void SetUser()
        {
            var userService = new UserService();
            var user = userService.GetLocal();
            User = user;
            IsLoggedIn = user != null;
        }
    }
}