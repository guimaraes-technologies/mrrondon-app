using Xamarin.Forms;

namespace MrRondon.Pages.Account
{
	public partial class InformationPage : ContentPage
	{
	    private readonly InformationPageModel _pageModel;
        
        public InformationPage (InformationPageModel pageModel)
		{
			InitializeComponent ();
		    BindingContext = _pageModel = pageModel;
		}

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
	    }
	}
}