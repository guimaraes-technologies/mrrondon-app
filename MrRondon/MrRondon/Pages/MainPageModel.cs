using MrRondon.Helpers;
using Xamarin.Forms;

namespace MrRondon.Pages
{
    public class MainPageModel : BasePageModel
    {
        public MainPageModel()
        {
            Title = Constants.AppName;
            ChangeActualCityCommand = new Command(async () => await ExecuteChangeActualCity(new MainPage()));
        }
    }
}