using Xamarin.Forms;

namespace MrRondon.Pages.Menu
{
    public partial class MenuPage : ContentPage
    {
        private readonly MenuPageModel _pageModel;

        public MenuPage()
        {
            InitializeComponent();
            BindingContext = _pageModel = new MenuPageModel();
        }
    }
}