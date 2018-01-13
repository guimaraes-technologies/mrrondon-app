using MrRondon.Services.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
	        //NavigationService.RemovePage(this);
	        //NavigationService.PopAsync();
	        //await NavigationService.PushAsync(CallBackPage);
	        return base.OnBackButtonPressed();
	    }
    }
}