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
            if (BindingContext == null) BindingContext = _pageModel = _pageModel ?? new ListCategoriesPageModel();
            else _pageModel = (ListCategoriesPageModel)BindingContext;
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