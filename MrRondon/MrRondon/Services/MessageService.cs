using MrRondon.Helpers;
using MrRondon.Services.Interfaces;
using Plugin.Toasts;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MrRondon.Services
{
    public class MessageService : IMessageService
    {
        public async Task ShowAsync(Exception ex)
        {
            if (ex == null) return;

            var error = ex.Message;
            if (!string.IsNullOrWhiteSpace(error))
            {
                await ShowAsync(error);
                return;
            }

            var detailError = ex.InnerException?.Message;
            if (!string.IsNullOrWhiteSpace(detailError))
            {
                await ShowAsync(detailError);
                return;
            }

            var detailErrorFurther = ex.InnerException?.InnerException?.Message;
            if (!string.IsNullOrWhiteSpace(detailErrorFurther)) await ShowAsync(detailErrorFurther);
        }

        public async Task ShowAsync(CustomError customError)
        {
            await Application.Current.MainPage.DisplayAlert(customError.Title, customError.Message, "OK");
        }

        public async Task ShowAsync(string message)
        {
            await Application.Current.MainPage.DisplayAlert(Constants.AppName, message, "OK");
        }

        public async Task ShowAsync(string title, string message)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, "OK");
        }

        public async Task<bool> ShowConfirmationAsync(string message, string accept, string cancel, string title)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        public async Task<bool> ShowConfirmationAsync(string message, string title)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, "OK", "Cancelar");
        }

        public async Task ToastAsync(string message, string title)
        {
            var notificator = DependencyService.Get<IToastNotificator>();
            var options = new NotificationOptions { Title = title, Description = message, ClearFromHistory = false };
            await notificator.Notify(options);
        }
    }
}