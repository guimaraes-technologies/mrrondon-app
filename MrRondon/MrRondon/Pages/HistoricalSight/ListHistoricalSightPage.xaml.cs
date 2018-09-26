using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MrRondon.Pages.HistoricalSight
{
    public partial class ListHistoricalSightPage : ContentPage
    {
        private readonly ListHistoricalSightPageModel _pageModel;

        public ListHistoricalSightPage(ListHistoricalSightPageModel pageModel)
        {
            InitializeComponent();
            BindingContext = _pageModel = pageModel;
        }

        protected override async void OnAppearing()
        {
            try
            {
                _pageModel.LoadCitiesCommand.Execute(null);

                base.OnAppearing();
            }
            catch (TaskCanceledException ex)
            {
                _pageModel.ExceptionService.TrackError(ex);
                await _pageModel.MessageService.ShowAsync("Informação",
                    "A requisição está demorando muito, verifique sua conexão com a internet.");
            }
            catch (Exception ex)
            {
                _pageModel.ExceptionService.TrackError(ex);
                await _pageModel.MessageService.ShowAsync(ex);
            }
            finally
            {
                _pageModel.IsLoading = false;
            }
        }
    }
}