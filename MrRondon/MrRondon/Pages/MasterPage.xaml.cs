using MrRondon.Helpers;
using MrRondon.Pages.Menu;
using MrRondon.ViewModels;
using Xamarin.Forms;

namespace MrRondon.Pages
{
    public partial class MasterPage : MasterDetailPage
    {
        private readonly MenuPageModel _pageModel;

        public MasterPage()
        {
            InitializeComponent();
            Title = Constants.AppName;
            BindingContext = _pageModel = new MenuPageModel();
        }

        public MasterPage(Page detail)
        {
            InitializeComponent();
            BindingContext = _pageModel = new MenuPageModel();
            Detail = detail;
            IsPresented = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _pageModel.LoadItemsCommand.Execute(null);
        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            IsPresented = false;
            ((ListView)sender).SelectedItem = null;
            var item = (MenuItemVm)e.SelectedItem;
            if (item == null) return;
            if (item.Type == MenuType.Login)
            {
                _pageModel.LoginCommand.Execute(null);
                return;
            }
            if (item.Type == MenuType.Logout)
            {
                _pageModel.LogoutCommand.Execute(null);
                return;
            }

            var page = MenuHelper.GetPage(item);
            if (!string.IsNullOrWhiteSpace(page.Title)) Title = page.Title;
            Detail = page;
        }
    }
}