using System;
using System.Diagnostics;
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
                base.OnAppearing();

                _pageModel.LoadCitiesCommand.Execute(null);
                _pageModel.LoadItemsCommand.Execute(null);
            }
            catch (TaskCanceledException ex)
            {
                Debug.WriteLine(ex);
                await _pageModel.MessageService.ShowAsync("Informação",
                    "A requisição está demorando muito, verifique sua conexão com a internet.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await _pageModel.MessageService.ShowAsync(ex.Message);
            }
            finally
            {
                _pageModel.IsLoading = false;
            }
        }
    }
}