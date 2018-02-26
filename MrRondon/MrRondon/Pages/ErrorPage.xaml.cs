using Xamarin.Forms;

namespace MrRondon.Pages
{
	public partial class ErrorPage : ContentPage
	{
	    private readonly ErrorPageModel _pageModel;

	    public ErrorPage()
	    {
	    }

	    public ErrorPage(ErrorPageModel error)
	    {
	        InitializeComponent();
	        BindingContext = _pageModel = error;
	    }

	    protected override bool OnBackButtonPressed()
	    {
	        //_pageModel.NavigationService.RemovePage(this);
	        //_pageModel.NavigationService.PopAsync();
	        //_pageModel.NavigationService.PushAsync();
	        return base.OnBackButtonPressed();
	    }
    }
}