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
    public class ListCategoriesPageModel : BasePageModel
    {
        public ListCategoriesPageModel()
        {
            Title = Constants.AppName;
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
        public ObservableRangeCollection<Entities.Category> Items { get; set; }

        private async Task ExecuteLoadItems()
        {
            try
            {
                if (IsLoading) return;

                NotHasItems = false;
                IsLoading = true;
                Items.Clear();
                var service = new CategoryService();
                var items = await service.GetAsync();
                NotHasItems = IsLoading && items != null && !items.Any();
                if (NotHasItems) ErrorMessage = "Nenhuma categoria encontrada";
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
            await MessageService.ToastAsync(category.Name, "You have selected");
        }
    }
}