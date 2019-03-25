using Android.OS;
using MrRondon.Services.Interfaces;

namespace MrRondon.Droid
{
    public class AndroidSettings : IAndroidSettings
    {
        public int GetAndroidApiLevel()
        {
            return (int)Build.VERSION.SdkInt;
        }
    }
}