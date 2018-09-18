using System;
using System.Collections.Generic;
using MrRondon.Auth;
using MrRondon.Services.Interfaces;
using Xamarin.Forms;

namespace MrRondon.Helpers
{
    public static class Startup
    {
        public static bool Run()
        {
            try
            {
                // É chamado pela UI thread pois o método é invocado por uma task
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await AccountManager.SetActualCity();
                    await AccountManager.GetAsync();
                });

                return true;
            }
            catch (Exception ex)
            {
                var exceptionService = DependencyService.Get<IExceptionService>();
                exceptionService.TrackError(ex, "Startup Method");
                return false;
            }
        }
    }
}