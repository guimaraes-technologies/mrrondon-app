using MrRondon.Droid;
using MrRondon.Services.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(AppManager))]
namespace MrRondon.Droid
{
    public class AppManager : IAppManager
    {
        public void Close()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}