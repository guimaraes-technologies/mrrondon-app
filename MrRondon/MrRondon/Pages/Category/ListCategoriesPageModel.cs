using DLToolkit.Forms.Controls;
using MrRondon.Helpers;
using MrRondon.Pages.Company;
using MrRondon.Pages.HistoricalSight;
using MrRondon.Services;
using MrRondon.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MrRondon.Pages.Category
{
    public class ListCategoriesPageModel : BasePageModel
    {
        public ListCategoriesPageModel()
        {
            Title = Constants.AppName;
            Items = new FlowObservableCollection<CategoryListVm>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItems());
            ItemSelectedCommand = new Command<ViewModels.CategoryListVm>(async (item) => await ExecuteItemSelected(item));
        }

        public ICommand LoadItemsCommand { get; set; }
        public ICommand ItemSelectedCommand { get; set; }

        private bool _notHhasItems;
        public bool NotHasItems
        {
            get => _notHhasItems;
            set => SetProperty(ref _notHhasItems, value);
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        private FlowObservableCollection<CategoryListVm> _items;
        public FlowObservableCollection<CategoryListVm> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        private async Task ExecuteLoadItems()
        {
            try
            {
                if (IsLoading) return;
                IsLoading = true;

                NotHasItems = false;
                var service = new CategoryService();
                var items = await service.GetAsync();
                NotHasItems = IsLoading && items != null && !items.Any();
                if (NotHasItems) ErrorMessage = "Nenhuma categoria encontrada";
                Items.AddRange(items);
            }
            catch (TaskCanceledException ex)
            {
                ExceptionService.TrackError(ex);
                await MessageService.ShowAsync("Informação", "A requisição está demorando muito, verifique sua conexão com a internet.");
            }
            catch (Exception ex)
            {
                ExceptionService.TrackError(ex);
                await MessageService.ShowAsync($"Não foi possível obter as categorias\n{ex.Message}");
            }
            finally
            {
                IsLoading = false;
                IsPresented = false;
            }
        }

        private async Task ExecuteItemSelected(CategoryListVm category)
        {
            if (category.SubCategoryId == Constants.HistoricalSightId)
            {
                await NavigationService.PushAsync(new ListHistoricalSightPage(new ListHistoricalSightPageModel()));
                return;
            }
            if (category.HasSubCategory)
            {
                var model = new ListSubCategoriesPageModel(category);
                var page = new ListSubCategoriesPage(model);
                await NavigationService.PushAsync(page);
                return;
            }

            var pageModel = new ListCompaniesPageModel(category.SubCategoryId, category.Name);
            pageModel.LoadCitiesCommand.Execute(null);
            await NavigationService.PushAsync(new ListCompaniesPage(pageModel));
        }
    }
}