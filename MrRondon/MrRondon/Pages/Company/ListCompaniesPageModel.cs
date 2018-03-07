using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MrRondon.Auth;
using MrRondon.Helpers;
using MrRondon.Services;
using Xamarin.Forms;

namespace MrRondon.Pages.Company
{
    public class ListCompaniesPageModel : BasePageModel
    {
        public ListCompaniesPageModel(int segmentId)
        {
            CategoryId = segmentId;
            Items = new ObservableRangeCollection<Entities.Company>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItems());
            ItemSelectedCommand = new Command<Entities.Company>(async (item) => await ExecuteLoadItem(item));
            LoadCitiesCommand = new Command<int>(async (subCategoryId) => await ExecuteLoadCities(subCategoryId));
            ChangeActualCityCommand = new Command(async () => await ExecuteChangeActualCity(new ListCompaniesPage(this)));
        }

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

        private int _categoryId;
        public int CategoryId
        {
            get => _categoryId;
            set => SetProperty(ref _categoryId, value);
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

                var selectedItem = Cities[_cityIndex] ?? AccountManager.DefaultSetting.City;
                CurrentCity = selectedItem;
                ApplicationManager<Entities.City>.AddOrUpdate("city", selectedItem);
                LoadItemsCommand.Execute(null);
            }
        }

        public ICommand LoadItemsCommand { get; set; }
        public ICommand ItemSelectedCommand { get; set; }

        private ObservableRangeCollection<Entities.Company> _items;
        public ObservableRangeCollection<Entities.Company> Items
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
                var service = new CompanyService();
                var items = await service.GetAsync(CategoryId, CurrentCity.CityId, Search);
                NotHasItems = IsLoading && items != null && !items.Any();
                if (NotHasItems) ErrorMessage = $"Nenhuma empresa encontrada em {CurrentCity.Name}.";
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

        protected async Task ExecuteLoadCities(int subCategoryId)
        {
            try
            {
                if (IsLoading) return;
                IsLoading = true;
                var items = await AccountManager.GetHasCompanyAsync(subCategoryId);
                Cities.ReplaceRange(items);
                CityNames = new List<string>(items.Select(s => s.Name));

                CitySelectedIndex = CityNames.Any(a => a.ToLower().Equals(CurrentCity.Name.ToLower()))
                    ? CityNames.IndexOf(CurrentCity.Name) : 0;
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

        private async Task ExecuteLoadItem(Entities.Company model)
        {
            try
            {
                var service = new CompanyService();
                var item = await service.GetByIdAsync(model.CompanyId);
                var pageModel = new CompanyDetailsPageModel(item);
                await NavigationService.PushAsync(new CompanyDetailsPage(pageModel));
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