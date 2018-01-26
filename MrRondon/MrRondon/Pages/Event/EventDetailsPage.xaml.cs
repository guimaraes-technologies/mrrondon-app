using Xamarin.Forms;

namespace MrRondon.Pages.Event
{
	public partial class EventDetailsPage : ContentPage
	{
        public EventDetailsPage (EventDetailsPageModel pageModel)
		{
			InitializeComponent ();
		    BindingContext = pageModel;
        }
	}
}