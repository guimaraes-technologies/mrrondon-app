using MrRondon.Services.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(MrRondon.Droid.AppVersion))]

namespace MrRondon.Droid
{
    public class AppVersion : IAppVersion
    {
        public string GetVersion()
        {
            var context = Android.App.Application.Context;

            var manager = context.PackageManager;
            var info = manager.GetPackageInfo(context.PackageName, 0);

            return info.VersionName;
        }

        public int GetBuild()
        {
            var context = Android.App.Application.Context;
            var manager = context.PackageManager;
            var info = manager.GetPackageInfo(context.PackageName, 0);

            return info.VersionCode;
        }
    }
}