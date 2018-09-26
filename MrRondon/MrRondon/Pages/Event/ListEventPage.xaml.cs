using System;
using System.Threading.Tasks;
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

        protected override async void OnAppearing()
        {
            try
            {
                _pageModel.LoadCitiesCommand.Execute(null);
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