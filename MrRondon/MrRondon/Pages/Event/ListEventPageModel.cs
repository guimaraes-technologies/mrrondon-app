using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MrRondon.Auth;
using MrRondon.Entities;
using MrRondon.Helpers;
using MrRondon.Services;
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

        private string _city;
        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        private string _searchBar;
        public string Search
        {
            get => _searchBar;
            set => SetProperty(ref _searchBar, value);
        }

        public ICommand LoadItemsCommand { get; set; }
        public ICommand ItemSelectedCommand { get; set; }

        private ObservableRangeCollection<Entities.Event> _items;
        public ObservableRangeCollection<Entities.Event> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public List<string> CityNames { get; private set; }

        public ListEventPageModel()
        {
            Title = Constants.AppName;
            Items = new ObservableRangeCollection<Entities.Event>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItems());
            ItemSelectedCommand = new Command<Entities.Event>(async (item) => await ExecuteItemSelected(item));
            var cities = AccountManager.GetCities().Result;
            CityNames = new List<string>(cities.Select(s => s.Name));
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
                var items = await service.GetAsync(Search);
                NotHasItems = IsLoading && items != null && !items.Any();
                if (NotHasItems) ErrorMessage = "Nenhum evento encontrado";
                Items.ReplaceRange(items);
                await Task.Delay(100);
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

        private async Task ExecuteItemSelected(Entities.Event model)
        {
            var pageModel = new EventDetailsPageModel(model);
            await NavigationService.PushAsync(new EventDetailsPage(pageModel));
        }
    }
}