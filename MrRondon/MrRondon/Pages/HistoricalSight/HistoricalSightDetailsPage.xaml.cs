using Xamarin.Forms;

namespace MrRondon.Pages.HistoricalSight
{
	public partial class HistoricalSightDetailsPage : ContentPage
	{
	    private readonly HistoricalSightDetailsPageModel _pageModel;

		public HistoricalSightDetailsPage (HistoricalSightDetailsPageModel pageModel)
		{
			InitializeComponent ();
		    BindingContext = _pageModel = pageModel;
		}
	}
}