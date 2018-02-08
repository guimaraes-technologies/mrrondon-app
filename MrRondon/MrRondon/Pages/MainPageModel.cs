using System.Windows.Input;
using MrRondon.Helpers;

namespace MrRondon.Pages
{
    public class MainPageModel : BasePageModel
    {
        public ICommand AboutCommand { get; set; }
        public ICommand ChangeActualCityCommand { get; set; }
        
        public MainPageModel()
        {
            Title = Constants.AppName;
        }
    }
}