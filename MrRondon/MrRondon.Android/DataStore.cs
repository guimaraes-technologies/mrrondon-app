using MrRondon.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(MrRondon.Droid.DataStore))]
namespace MrRondon.Droid
{
    public class DataStore : IDataStore
    {
        private string _diretorioDb;
        public string Path
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_diretorioDb)) _diretorioDb = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                return _diretorioDb;
            }
        }

        private SQLite.Net.Interop.ISQLitePlatform _platform;
        public SQLite.Net.Interop.ISQLitePlatform Platform => _platform ?? (_platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid());

        public DataStore() { }
    }
}