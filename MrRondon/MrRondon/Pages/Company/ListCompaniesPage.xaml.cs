using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MrRondon.Pages.Company
{
	public partial class ListCompaniesPage : ContentPage
	{
	    private readonly ListCompaniesPageModel _pageModel;

        public ListCompaniesPage (ListCompaniesPageModel pageModel)
		{
			InitializeComponent ();
		    BindingContext = _pageModel = pageModel;
		}

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
            if(_pageModel.Items.Count == 0) _pageModel.LoadItemsCommand.Execute(null);
	    }
	}
}