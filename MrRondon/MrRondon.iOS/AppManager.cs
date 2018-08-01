using System.Threading;
using MrRondon.iOS;
using MrRondon.Services.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(AppManager))]
namespace MrRondon.iOS
{
    public class AppManager : IAppManager
    {
        public void Close()
        {
            Thread.CurrentThread.Abort();
        }
    }
}