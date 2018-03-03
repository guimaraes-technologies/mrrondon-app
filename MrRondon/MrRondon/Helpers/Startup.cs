using System;
using MrRondon.Auth;

namespace MrRondon.Helpers
{
    public static class Startup
    {
        public static bool Run()
        {
            try
            {
                AccountManager.SetActualCity();
                AccountManager.GetAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}