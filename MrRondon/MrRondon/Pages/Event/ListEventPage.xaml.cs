using Xamarin.Forms;

namespace MrRondon.Pages.Event
{
    public partial class ListEventPage : ContentPage
    {
        private readonly ListEventPageModel _pageModel;

        public ListEventPage()
        {
            if (Device.RuntimePlatform.Equals(Device.iOS)) Icon = "ic_event";
            InitializeComponent();
            BindingContext = _pageModel = new ListEventPageModel();
        }

        protected override void OnAppearing()
        {
            _pageModel.LoadCitiesCommand.Execute(null);
            _pageModel.LoadItemsCommand.Execute(null);

            base.OnAppearing();
        }
    }
}