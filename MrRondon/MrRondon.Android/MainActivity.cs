using Android.App;
using Android.Content.PM;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using Plugin.Permissions;
using Plugin.Toasts;
using Xamarin;
using Xamarin.Forms;

namespace MrRondon.Droid
{
    [Activity(Label = "@string/app_name", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity = this;

            base.OnCreate(bundle);

            Forms.SetFlags("FastRenderers_Experimental");
            Forms.Init(this, bundle);

            //Map
            FormsMaps.Init(this, bundle);

            //ImageCircle
            ImageCircleRenderer.Init();

            DependencyService.Register<ToastNotification>();
            ToastNotification.Init(this, new PlatformOptions { SmallIconDrawable = Android.Resource.Drawable.IcDialogInfo });

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            //base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}