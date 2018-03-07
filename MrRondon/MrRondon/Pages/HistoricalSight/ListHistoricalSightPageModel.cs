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
                _cityIndex = value;
                Notify(nameof(CitySelectedIndex));

                var selectedItem = Cities[_cityIndex] ?? AccountManager.DefaultSetting.City;
                CurrentCity = selectedItem;
                ApplicationManager<Entities.City>.AddOrUpdate("city", selectedItem);
                LoadItemsCommand.Execute(null);
            }
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
            LoadItemsCommand = new Command(async () => await ExecuteLoadItems());
            LoadCitiesCommand = new Command(async () => await ExecuteLoadCities());
            ItemSelectedCommand = new Command<Entities.HistoricalSight>(async (item) => await ExecuteItemSelected(item));
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
                if (NotHasItems) ErrorMessage = "Nenhum patrimônio histórico encontrado";
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
                    ? CityNames.IndexOf(CurrentCity.Name)
                    : 1;
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
            try
            {
                if (IsLoading) return;
                IsLoading = true;
                var service = new HistoricalSightService();
                var item = await service.GetByIdAsync(model.HistoricalSightId);
                var pageModel = new HistoricalSightDetailsPageModel(item);
                await NavigationService.PushAsync(new HistoricalSightDetailsPage(pageModel));
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
    }
}