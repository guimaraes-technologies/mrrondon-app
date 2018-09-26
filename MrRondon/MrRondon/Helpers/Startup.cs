using System;
using MrRondon.Auth;
using MrRondon.Services.Interfaces;
using Xamarin.Forms;

namespace MrRondon.Helpers
{
    public static class Startup
    {
        public static void Run()
        {
            try
            {
                // É chamado pela UI thread pois o método é invocado por uma task
                Device.BeginInvokeOnMainThread(async () =>
                {
                    //await AccountManager.SetActualCity();
                    await AccountManager.GetAsync();
                });
            }
            catch (Exception ex)
            {
                var exceptionService = DependencyService.Get<IExceptionService>();
                exceptionService.TrackError(ex, "Startup Method");
            }
        }
    }
}