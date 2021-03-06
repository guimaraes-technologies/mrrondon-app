﻿using MrRondon.Auth;
using MrRondon.Helpers;
using MrRondon.Pages.Category;
using MrRondon.Services;
using MrRondon.Services.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
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
                var selectedItem = Cities[_cityIndex];

                if (selectedItem != null && _cityIndex > 0)
                {
                    CurrentCity = selectedItem;
                    LoadItemsCommand.Execute(null);
                }
                else
                {
                    ErrorMessage = "Nehuma cidade foi informada";
                    Items.Clear();
                }
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
                if (IsLoading || CurrentCity == null || CurrentCity.CityId == 0) return;
                NotHasItems = false;
                IsLoading = true;
                Items.Clear();

                var service = new HistoricalSightRest();
                var result = await service.GetAsync(CurrentCity.CityId, Search);
                if (result.IsValid)
                {
                    NotHasItems = result.Value == null || !result.Value.Any();
                    if (NotHasItems) ErrorMessage = $"Nenhum Memorial histórico encontrado em {CurrentCity.Name}";
                    Items = new ObservableRangeCollection<HistoricalSightDetailsPageModel>(result.Value.OrderBy(o => o.Name).Select(s => new HistoricalSightDetailsPageModel(s)));
                }
                else await MessageService.ShowAsync(result.Error.Title, result.Error.Message);
            }
            catch (TaskCanceledException ex)
            {
                ExceptionService.TrackError(ex);
                await MessageService.ShowAsync("Informação", "A requisição está demorando muito, verifique sua conexão com a internet.");
            }
            catch (Exception ex)
            {
                ExceptionService.TrackError(ex);
                await MessageService.ShowAsync($"Não foi possível obter os pontos turísticos\n{ex.Message}");
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
                var items = new List<Entities.City> { new Entities.City { CityId = 0, Name = "Selecione" } };
                items.AddRange(await AccountManager.GetHasHistoricalSightAsync());
                Cities.ReplaceRange(items);
                CityNames = new List<string>(items.Select(s => s.Name));

                CitySelectedIndex = 0;
                ErrorMessage = "Nehuma cidade foi informada";
                //CitySelectedIndex = CityNames.Any(a => a.ToLower() == CurrentCity.Name.ToLower())
                //    ? CityNames.IndexOf(CurrentCity.Name) : 0;
            }
            catch (Exception ex)
            {
                ExceptionService.TrackError(ex);
                await MessageService.ShowAsync($"Não foi possível obter as cidades com pontos turísticos\n{ex.Message}");
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
                var service = new HistoricalSightRest();
                var result = await service.GetByIdAsync(model.HistoricalSight.HistoricalSightId);

                if (result.IsValid)
                {
                    var pageModel = new HistoricalSightDetailsPageModel(result.Value);
                    await NavigationService.PushAsync(new HistoricalSightDetailsPage(pageModel));
                }
                else await MessageService.ShowAsync(result.Error.Title, result.Error.Message);
            }
            catch (Exception ex)
            {
                ExceptionService.TrackError(ex);
                await MessageService.ShowAsync($"Não foi possível obter os detalhes do ponto turístico\n{ex.Message}");
            }
            finally
            {
                IsLoading = false;
                IsPresented = false;
            }
        }
    }
}