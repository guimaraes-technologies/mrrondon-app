using Xamarin.Forms;

namespace MrRondon.Pages.Event
{
    public partial class ListEventPage : ContentPage
    {
        private readonly ListEventPageModel _pageModel;

        public ListEventPage()
        {
            InitializeComponent();
            if (BindingContext == null) BindingContext = _pageModel = _pageModel ?? new ListEventPageModel();
            else _pageModel = (ListEventPageModel)BindingContext;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_pageModel.Items.Count == 0) _pageModel.LoadItemsCommand.Execute(null);
        }
    }
}