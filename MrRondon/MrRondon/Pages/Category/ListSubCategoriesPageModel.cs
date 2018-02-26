using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MrRondon.Helpers;
using MrRondon.Pages.Company;
using MrRondon.Services;
using Xamarin.Forms;

namespace MrRondon.Pages.Category
{
    public class ListSubCategoriesPageModel : BasePageModel
    {
        public ListSubCategoriesPageModel(ViewModels.CategoryListVm category)
        {
            Title = Constants.AppName;
            Category = category;
            Items = new ObservableRangeCollection<Entities.SubCategory>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItems());
            ItemSelectedCommand = new Command<Entities.SubCategory>(async (item) => await ExecuteItemSelected(item));
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

        private ViewModels.CategoryListVm _category;
        public ViewModels.CategoryListVm Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        private ObservableRangeCollection<Entities.SubCategory> _items;
        public ObservableRangeCollection<Entities.SubCategory> Items
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
                var service = new SubCategoryService();
                var items = await service.GetAsync(Category.SubCategoryId);
                NotHasItems = IsLoading && items != null && !items.Any();
                if (NotHasItems) ErrorMessage = "Nenhuma sub categoria encontrada";
                Items.ReplaceRange(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await NavigationService.PushAsync(new ErrorPage(new ErrorPageModel(ex.Message, Title) { IsLoading = false }));
            }
            finally
            {
                IsLoading = false;
                IsPresented = false;
            }
        }

        private async Task ExecuteItemSelected(Entities.SubCategory category)
        {
            var pageModel = new ListCompaniesPageModel(category.SubCategoryId);
            pageModel.LoadCitiesCommand.Execute(null);
            await NavigationService.PushAsync(new ListCompaniesPage(pageModel));
        }
    }
}