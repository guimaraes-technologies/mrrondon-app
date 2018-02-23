using System;
using System.Threading.Tasks;
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
                AccountManager.GetCities();
                AccountManager.Signin();

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