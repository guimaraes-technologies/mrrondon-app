using Xamarin.Forms;

namespace MrRondon.Pages.Event
{
	public partial class EventDetailsPage : ContentPage
	{
	    private readonly EventDetailsPageModel _pageModel;

        public EventDetailsPage (EventDetailsPageModel pageModel)
		{
			InitializeComponent ();
            //Window.AddFlags(WindowManagerFlags.TranslucentNavigation);
            //Window.AddFlags(WindowManagerFlags.TranslucentStatus);
            
            BindingContext = _pageModel = pageModel;
        }
	}
}