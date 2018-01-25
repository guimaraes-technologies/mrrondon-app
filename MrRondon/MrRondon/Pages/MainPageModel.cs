using System.Threading.Tasks;
using System.Windows.Input;
using MrRondon.Helpers;
using Xamarin.Forms;

namespace MrRondon.Pages
{
    public class MainPageModel : BasePageModel
    {
        public ICommand OnCurrentPageChangedCommand { get; set; }

        public MainPageModel()
        {
            Title = Constants.AppName;
            //OnCurrentPageChangedCommand = new Command(async () => await OnCurrentPageChanged());
        }

        private async Task OnCurrentPageChanged()
        {
            var currentPage = NavigationService.GetCurrentPage();
            await Task.Delay(100);
        }
    }
}