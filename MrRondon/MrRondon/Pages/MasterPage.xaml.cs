using System;
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
            
            //menuTitle.Text = $"Bem Vindo(a), {_pageModel.MenuTitle}";
            //signinSignout.Text = _pageModel.SiginSignoutText;
        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MenuItemVm)e.SelectedItem;
            if (item == null) return;

            var page = MenuHelper.GetPage(item);
            if (!string.IsNullOrWhiteSpace(page.Title)) Title = page.Title;
            Detail = page;
            IsPresented = false;
            ((ListView)sender).SelectedItem = null;
        }
    }
}