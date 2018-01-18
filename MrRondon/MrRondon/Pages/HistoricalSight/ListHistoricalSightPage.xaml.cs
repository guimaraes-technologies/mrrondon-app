using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MrRondon.Pages.HistoricalSight
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListHistoricalSightPage : ContentPage
	{
	    private readonly ListHistoricalSightPageModel _pageModel;

        public ListHistoricalSightPage ()
		{
			InitializeComponent ();
		    if (BindingContext == null) BindingContext = _pageModel = _pageModel ?? new ListHistoricalSightPageModel();
		    else _pageModel = (ListHistoricalSightPageModel)BindingContext;
        }

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();

	        if (_pageModel.Items.Count == 0) _pageModel.LoadItemsCommand.Execute(null);
	    }
    }
}