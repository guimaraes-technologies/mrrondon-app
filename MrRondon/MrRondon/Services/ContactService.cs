using System.Threading.Tasks;
using MrRondon.Services.Rest;
using MrRondon.ViewModels;

namespace MrRondon.Services
{
    public class ContactService
    {
        public async Task<bool> SendAsync(ContactMessageVm contactMessage)
        {
            var service = new ContactRest();
            return await service.SendAsync(contactMessage);
        }
    }
}