using Foundation;
using MrRondon.Services.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(MrRondon.iOS.AppVersion))]
namespace MrRondon.iOS
{
    public class AppVersion : IAppVersion
    {
        public string GetVersion()
        {
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();
        }

        public int GetBuild()
        {
            return int.Parse(NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleVersion").ToString());
        }
    }
}