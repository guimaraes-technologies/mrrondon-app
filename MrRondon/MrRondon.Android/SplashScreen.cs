using Android.App;
using Android.Content;
using Android.Content.PM;

namespace MrRondon.Droid
{
    [Activity(Label = "@string/app_name", MainLauncher = true, NoHistory = true, Theme = "@style/Theme.Splash",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashScreen : Activity
    {
        protected override void OnResume()
        {
            base.OnResume();

            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}