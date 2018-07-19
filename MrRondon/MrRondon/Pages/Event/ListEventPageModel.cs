using MrRondon.Auth;
using MrRondon.Helpers;
using MrRondon.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MrRondon.Pages.Event
{
    public class ListEventPageModel : BasePageModel
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

        private string _searchBar;
        public string Search
        {
            get => _searchBar;
            set => SetProperty(ref _searchBar, value);
        }

        private int _cityIndex;
        public int CitySelectedIndex
        {
            get => _cityIndex;
            set
            {
                _cityIndex = value < 0 ? 0 : value;
                Notify(nameof(CitySelectedIndex));
                var lastFilteredCity = ApplicationManager<Entities.City>.Find("city");
                var selectedItem = Cities[_cityIndex] ?? AccountManager.DefaultSetting.City;

                if (selectedItem.CityId == lastFilteredCity?.CityId)
                {
                    LoadItemsCommand.Execute(null);
                    return;
                }

                if (Cities.Any(a => a.CityId == selectedItem.CityId))
                {
                    CurrentCity = selectedItem;
                    ApplicationManager<Entities.City>.AddOrUpdate("city", selectedItem);
                }

                LoadItemsCommand.Execute(null);
            }
        }

        public ICommand LoadItemsCommand { get; set; }
        public ICommand LoadMoreCommand { get; set; }
        public ICommand ItemSelectedCommand { get; set; }

        private ObservableRangeCollection<EventDetailsPageModel> _items;
        public ObservableRangeCollection<EventDetailsPageModel> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public ListEventPageModel()
        {
            Title = Constants.AppName;
            Items = new ObservableRangeCollection<EventDetailsPageModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItems());
            LoadMoreCommand = new Command(async () => await ExecuteLoadMoreItems());
            LoadCitiesCommand = new Command(async () => await ExecuteLoadCities());
            ItemSelectedCommand = new Command<EventDetailsPageModel>(async (item) => await ExecuteItemSelected(item));
            ChangeActualCityCommand = new Command(async () => await ExecuteChangeActualCity(new ListEventPage()));
        }

        private async Task ExecuteLoadItems()
        {
            try
            {
                if (IsLoading) return;

                NotHasItems = false;
                IsLoading = true;
                Items.Clear();
                var service = new EventService();
                var items = await service.GetAsync(CurrentCity.CityId, Search, Items.Count);
                NotHasItems = IsLoading && items != null && !items.Any();
                if (NotHasItems) ErrorMessage = $"Nenhum evento encontrado em {CurrentCity.Name}";

                if (items == null) return;
                Items.AddRange(items.Select(s => new EventDetailsPageModel(s)));
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

        private async Task ExecuteLoadMoreItems()
        {
            try
            {
                if (IsLoading) return;

                NotHasItems = false;
                IsLoading = true;
                Items.Clear();
                var service = new EventService();
                var items = await service.GetAsync(CurrentCity.CityId, Search, Items.Count);

                if (items == null) return;
                Items.AddRange(items.Select(s => new EventDetailsPageModel(s)));
            }
            catch (TaskCanceledException ex)
            {
                ExceptionService.TrackError(ex);
                await MessageService.ShowAsync("Informação", "A requisição está demorando muito, verifique sua conexão com a internet.");
            }
            catch (Exception ex)
            {
                ExceptionService.TrackError(ex);
            }
            finally
            {
                IsLoading = false;
                IsPresented = false;
            }
        }

        protected async Task ExecuteLoadCities()
        {
            try
            {
                if (IsLoading) return;
                IsLoading = true;
                var items = await AccountManager.GetHasEventAsync();
                Cities.ReplaceRange(items);
                CityNames = new List<string>(items.Select(s => s.Name));

                CitySelectedIndex = CityNames.Any(a => a.ToLower().Equals(CurrentCity.Name.ToLower()))
                    ? CityNames.IndexOf(CurrentCity.Name)
                    : 0;
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

        private async Task ExecuteItemSelected(EventDetailsPageModel model)
        {
            try
            {
                if (IsLoading) return;
                IsLoading = true;

                var service = new EventService();
                var item = await service.GetAsync(model.Event.EventId);
                var pageModel = new EventDetailsPageModel(item);

                await NavigationService.PushAsync(new EventDetailsPage(pageModel));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
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