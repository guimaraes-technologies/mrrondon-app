using MrRondon.Helpers;
using MrRondon.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MrRondon.Pages.Event
{
    public class FavoriteEventsPageModel : BasePageModel
    {
        public FavoriteEventsPageModel()
        {
            Title = "Eventos Favoritos";
            Items = new ObservableRangeCollection<Entities.FavoriteEvent>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItems());
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

        private ObservableRangeCollection<Entities.FavoriteEvent> _items;
        public ObservableRangeCollection<Entities.FavoriteEvent> Items
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
                var service = new FavoriteEventService();
                var result = await service.GetAsync();
                if (result.IsValid)
                {
                    NotHasItems = IsLoading && result.Value != null && !result.Value.Any();
                    if (NotHasItems) ErrorMessage = "Nenhum evento foi marcado como favorito";
                    Items.ReplaceRange(result.Value.OrderBy(o => o.Event.Name).ToList());
                }
                else await MessageService.ShowAsync(result.Error);
            }
            catch (TaskCanceledException ex)
            {
                ExceptionService.TrackError(ex);
                await MessageService.ShowAsync("Informação", "A requisição está demorando muito, verifique sua conexão com a internet.");
            }
            catch (Exception ex)
            {
                ExceptionService.TrackError(ex);
                await MessageService.ShowAsync(ex);
            }
            finally
            {
                IsLoading = false;
                IsPresented = false;
            }
        }
    }
}