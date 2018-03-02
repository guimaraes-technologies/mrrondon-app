using System.Linq;
using MrRondon.Entities;

namespace MrRondon.Pages.Account
{
    public class InformationPageModel : BasePageModel
    {
        public InformationPageModel(User user)
        {
            Title = "Minhas informações";
            User = user;
        }

        public User User { get; private set; }
        public bool NotHasContacts => !User?.Contacts?.Any() ?? true;
    }
}