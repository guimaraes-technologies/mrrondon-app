using Xamarin.Forms;

namespace MrRondon.Pages.Category
{
    public partial class ListSubCategoriesPage : ContentPage
    {
        private readonly ListSubCategoriesPageModel _pageModel;

        public ListSubCategoriesPage(ListSubCategoriesPageModel pageModel)
        {
            InitializeComponent();
            if (BindingContext == null) BindingContext = _pageModel = pageModel;
            else _pageModel = (ListSubCategoriesPageModel)BindingContext;
        }

        protected override void OnAppearing()
        {
            if (_pageModel.Items.Count == 0) _pageModel.LoadItemsCommand.Execute(null);
            base.OnAppearing();
        }
    }
}