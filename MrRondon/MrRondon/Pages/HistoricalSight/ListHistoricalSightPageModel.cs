using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MrRondon.Helpers;
using MrRondon.Services;
using Xamarin.Forms;

namespace MrRondon.Pages.HistoricalSight
{
    public class ListHistoricalSightPageModel : BasePageModel
    {
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

        private string _search;

        public string Search
        {
            get => _search;
            set => SetProperty(ref _search, value);
        }

        public ICommand LoadItemsCommand { get; set; }
        public ICommand ItemSelectedCommand { get; set; }

        private ObservableRangeCollection<Entities.HistoricalSight> _items;
        public ObservableRangeCollection<Entities.HistoricalSight> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public ListHistoricalSightPageModel()
        {
            Title = Constants.AppName;
            Items = new ObservableRangeCollection<Entities.HistoricalSight>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemSelectedCommand = new Command<Entities.HistoricalSight>(async (item) => await ExecuteItemSelected(item));
        }

        private async Task ExecuteLoadItemsCommand()
        {
            try
            {
                if (IsLoading) return;
                NotHasItems = false;
                IsLoading = true;
                Items.Clear();
                var service = new HistoricalSightService();
                var items = await service.GetAsync(Search);
                NotHasItems = IsLoading && items != null && !items.Any();
                if (NotHasItems) ErrorMessage = "Nenhum local histórico encontrado";
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

        private async Task ExecuteItemSelected(Entities.HistoricalSight model)
        {
            var service = new HistoricalSightService();
            var item = await service.GetByIdAsync(model.HistoricalSightId);
            var pageModel = new HistoricalSightDetailsPageModel(item);
            await NavigationService.PushAsync(new HistoricalSightDetailsPage(pageModel));
        }
    }
}