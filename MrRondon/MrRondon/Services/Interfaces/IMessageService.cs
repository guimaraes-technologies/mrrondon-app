using System.Threading.Tasks;
using MrRondon.Helpers;

namespace MrRondon.Services.Interfaces
{
    public interface IMessageService
    {
        Task ShowAsync(string message);
        Task ShowAsync(string title, string message);
        Task<bool> ShowConfirmationAsync(string message, string accept, string cancel, string title = Constants.AppName);
        Task<bool> ShowConfirmationAsync(string message, string title = Constants.AppName);
        Task ToastAsync(string message, string title = "");
    }
}