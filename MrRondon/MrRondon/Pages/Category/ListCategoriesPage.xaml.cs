using Xamarin.Forms;

namespace MrRondon.Pages.Category
{
	public partial class ListCategoriesPage : ContentPage
	{
	    //private readonly ListCategoriesPageModel _pageModel;

        public ListCategoriesPage()
        {
            InitializeComponent();
            //if (BindingContext == null) BindingContext = _pageModel = _pageModel ?? new ListCategoriesPageModel();
            //else _pageModel = (ListCategoriesPageModel)BindingContext;
        }
    }
}