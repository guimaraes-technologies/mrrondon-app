using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MrRondon.Helpers;
using MrRondon.Services;
using Xamarin.Forms;

namespace MrRondon.Pages.Category
{
    public class ListSubCategoriesPageModel : BasePageModel
    {
        public ListSubCategoriesPageModel(Entities.Category category)
        {
            Title = Constants.AppName;
            Category = category;
            Items = new ObservableRangeCollection<Entities.Category>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItems());
            ItemSelectedCommand = new Command<Entities.Category>(async (item) => await ExecuteItemSelected(item));
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

        private Entities.Category _category;
        public Entities.Category Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        private ObservableRangeCollection<Entities.Category> _items;
        public ObservableRangeCollection<Entities.Category> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        private async Task ExecuteLoadItems()
        {
            try
            {
                if (IsLoading) return;

                NotHasItems = false;
                IsLoading = true;
                Items.Clear();
                var service = new SubCategoryService();
                var items = await service.GetAsync(Category.CategoryId);
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

        private async Task ExecuteItemSelected(Entities.Category category)
        {
            await MessageService.ShowAsync(category.Name, $"You have selected {category.Name}");
        }
    }
}