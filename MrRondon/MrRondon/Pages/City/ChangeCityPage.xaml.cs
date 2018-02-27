using Xamarin.Forms;

namespace MrRondon.Pages.City
{
    public partial class ChangeCityPage : ContentPage
    {
        private readonly ChangeCityPageModel _pageModel;

        public ChangeCityPage(ChangeCityPageModel pageModel)
        {
            InitializeComponent();
            _pageModel = pageModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _pageModel.LoadCitiesCommand.Execute(null);
        }
    }
}