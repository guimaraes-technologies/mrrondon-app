using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MrRondon.Auth;
using MrRondon.Entities;
using MrRondon.Helpers;
using MrRondon.Services.Interfaces;
using Xamarin.Forms;

namespace MrRondon.Pages
{
    public class BasePageModel : ObservableObject
    {
        protected IMessageService MessageService;
        protected INavigationService NavigationService;

        public ICommand LoadCitiesCommand { get; set; }
        private string _title = Constants.AppName;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private bool _isPresented;
        public bool IsPresented
        {
            get => _isPresented;
            set => SetProperty(ref _isPresented, value);
        }

        private City _currentCity;
        public City CurrentCity
        {
            get => _currentCity;
            set => SetProperty(ref _currentCity, value);
        }

        public ObservableRangeCollection<City> Cities { get; set; }
        public List<string> CityNames { get; private set; }

        private int _cityIndex;
        public int CitySelectedIndex
        {
            get => _cityIndex;
            set
            {
                if (_cityIndex == value) return;

                _cityIndex = value;
                Notify(nameof(CitySelectedIndex));

                var selectedItem = Cities[_cityIndex];
                CurrentCity = selectedItem;
            }
        }

        protected BasePageModel()
        {
            IsPresented = false;
            IsLoading = false;
            Title = Constants.AppName;
            CurrentCity = ApplicationManager<City>.Find("city") ?? GetDefaultCity();
            Cities = new ObservableRangeCollection<City>();
            MessageService = DependencyService.Get<IMessageService>();
            NavigationService = DependencyService.Get<INavigationService>();
        }

        private static City GetDefaultCity()
        {
            var city = AccountManager.DefaultSetting.City;

            return city;
        }

        protected async Task ExecuteLoadCities()
        {
            try
            {
                if (IsLoading) return;
                IsLoading = true;
                var items = await AccountManager.GetCities();
                Cities.ReplaceRange(items);
                CityNames = new List<string>(items.Select(s => s.Name));
                CitySelectedIndex = CityNames.Any(a => a.ToLower().Equals(CurrentCity.Name.ToLower())) ? CityNames.IndexOf(CurrentCity.Name) : 0;
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