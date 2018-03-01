using Xamarin.Forms;

namespace MrRondon.Pages.Event
{
	public partial class EventDetailsPage : ContentPage
	{
	    private readonly EventDetailsPageModel _pageModel;

        public EventDetailsPage (EventDetailsPageModel pageModel)
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