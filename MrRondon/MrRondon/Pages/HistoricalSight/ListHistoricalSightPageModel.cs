using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MrRondon.Auth;
using MrRondon.Helpers;
using MrRondon.Pages.Category;
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
        public ICommand ItemSelectedCommand { get; set; }

        private ObservableRangeCollection<HistoricalSightDetailsPageModel> _items;
        public ObservableRangeCollection<HistoricalSightDetailsPageModel> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public ListHistoricalSightPageModel()
        {
            Title = Constants.AppName;
            Items = new ObservableRangeCollection<HistoricalSightDetailsPageModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItems());
            LoadCitiesCommand = new Command(async () => await ExecuteLoadCities());
            ItemSelectedCommand = new Command<HistoricalSightDetailsPageModel>(async (item) => await ExecuteItemSelected(item));
            ChangeActualCityCommand = new Command(async () => await ExecuteChangeActualCity(new ListCategoriesPage()));
        }

        private async Task ExecuteLoadItems()
        {
            try
            {
                if (IsLoading) return;
                NotHasItems = false;
                IsLoading = true;
                Items.Clear();
                var service = new HistoricalSightService();
                var items = await service.GetAsync(CurrentCity.CityId, Search);
                NotHasItems = IsLoading && items != null && !items.Any();
                if (NotHasItems) ErrorMessage = $"Nenhum Memorial histórico encontrado em {CurrentCity.Name}";
                Items = new ObservableRangeCollection<HistoricalSightDetailsPageModel>(items.Select(s => new HistoricalSightDetailsPageModel(s)));
            }
            catch (TaskCanceledException ex)
            {
                ExceptionService.TrackError(ex);
                await MessageService.ShowAsync("Informação", "A requisição está demorando muito, verifique sua conexão com a internet.");
            }
            catch (Exception ex)
            {
                ExceptionService.TrackError(ex);
                await MessageService.ShowAsync(ex.Message);
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
                var items = await AccountManager.GetHasHistoricalSightAsync();
                Cities.ReplaceRange(items);
                CityNames = new List<string>(items.Select(s => s.Name));

                CitySelectedIndex = CityNames.Any(a => a.ToLower().Equals(CurrentCity.Name.ToLower()))
                    ? CityNames.IndexOf(CurrentCity.Name) : 0;
            }
            catch (Exception ex)
            {
                ExceptionService.TrackError(ex);
                await MessageService.ShowAsync(ex.Message);
            }
            finally
            {
                IsLoading = false;
                IsPresented = false;
            }
        }

        private async Task ExecuteItemSelected(HistoricalSightDetailsPageModel model)
        {
            try
            {
                if (IsLoading) return;
                IsLoading = true;
                var service = new HistoricalSightService();
                var item = await service.GetByIdAsync(model.HistoricalSight.HistoricalSightId);
                var pageModel = new HistoricalSightDetailsPageModel(item);
                await NavigationService.PushAsync(new HistoricalSightDetailsPage(pageModel));
            }
            catch (Exception ex)
            {
                ExceptionService.TrackError(ex);
                await MessageService.ShowAsync(ex.Message);
            }
            finally
            {
                IsLoading = false;
                IsPresented = false;
            }
        }
    }
}