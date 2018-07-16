using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MrRondon.Pages.Category
{
    public partial class ListCategoriesPage : ContentPage
    {
        private readonly ListCategoriesPageModel _pageModel;

        public ListCategoriesPage()
        {
            InitializeComponent();
            if (Device.RuntimePlatform.Equals(Device.iOS)) Icon = "ic_category";
            BindingContext = _pageModel = new ListCategoriesPageModel();
        }

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                _pageModel.LoadItemsCommand.Execute(null);
            }
            catch (TaskCanceledException ex)
            {
                _pageModel.ExceptionService.TrackError(ex);
                await _pageModel.MessageService.ShowAsync("Informação", "A requisição está demorando muito, verifique sua conexão com a internet.");
            }
            catch (Exception ex)
            {
                _pageModel.ExceptionService.TrackError(ex);
                await _pageModel.MessageService.ShowAsync(ex.Message);
            }
        }
    }
}