using Xamarin.Forms;

namespace MrRondon.Pages.HistoricalSight
{
    public partial class ListHistoricalSightPage : ContentPage
    {
        private readonly ListHistoricalSightPageModel _pageModel;

        public ListHistoricalSightPage()
        {
            InitializeComponent();
            //if (BindingContext == null) BindingContext = _pageModel = _pageModel ?? new ListHistoricalSightPageModel();
            //else _pageModel = (ListHistoricalSightPageModel)BindingContext;
        }
    }
}