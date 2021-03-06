﻿using MrRondon.Helpers;
using MrRondon.Pages.City;
using MrRondon.Services.Interfaces;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MrRondon.Pages
{
    public class BasePageModel : ObservableObject
    {
        public IExceptionService ExceptionService;
        public IMessageService MessageService;
        protected INavigationService NavigationService;

        public ICommand LoadCitiesCommand { get; set; }
        public ICommand ChangeActualCityCommand { get; set; }
        public ICommand GoToSystemSettingsCommand { get; set; }
        //public ICommand HasPermissionCommand { get; set; }
        public ICommand GotoMainPageCommand { get; set; }

        public bool LocationGranted { get; private set; }

        private string _title = Constants.AppName;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _menuTitle = "Visitante";
        public string MenuTitle
        {
            get => _menuTitle;
            set
            {
                if (_menuTitle == value) return;
                SetProperty(ref _menuTitle, value);
                var account = Auth.Account.Current;
                if (account.IsValid) MenuTitle = account.User.FullName;
            }
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

        private Entities.City _currentCity;
        public Entities.City CurrentCity
        {
            get => _currentCity;
            set => SetProperty(ref _currentCity, value);
        }

        private ObservableRangeCollection<Entities.City> _cities;
        public ObservableRangeCollection<Entities.City> Cities
        {
            get => _cities;
            set => SetProperty(ref _cities, value);
        }

        private List<string> _cityName;
        public List<string> CityNames
        {
            get => _cityName;
            set => SetProperty(ref _cityName, value);
        }

        protected BasePageModel()
        {
            IsPresented = false;
            IsLoading = false;
            LocationGranted = false;
            Title = Constants.AppName;
            //CurrentCity = ApplicationManager<Entities.City>.Find("city") ?? AccountManager.DefaultSetting.City;
            Cities = new ObservableRangeCollection<Entities.City>();

            ExceptionService = DependencyService.Get<IExceptionService>();
            MessageService = DependencyService.Get<IMessageService>();
            NavigationService = DependencyService.Get<INavigationService>();
            GotoMainPageCommand = new Command(async () => await ExecuteGotoMainPage());
            GoToSystemSettingsCommand = new Command(GoToSystemSettings);
        }

        public static void GoToSystemSettings()
        {
            CrossPermissions.Current.OpenAppSettings();
        }

        protected async Task ExecuteChangeActualCity(Page previousPage)
        {
            var pageModel = new ChangeCityPageModel(previousPage);
            await NavigationService.PushModalAsync(new ChangeCityPage(pageModel));
        }

        protected async Task ExecuteGotoMainPage()
        {
            await NavigationService.PushModalAsync(new MainPage());
        }

        #region IDisposable Members
        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                // Set the _disposed flag to prevent subsequent disposals.
                _disposed = true;
            }
        }

        ~BasePageModel()
        {
            Dispose(false);
        }

        #endregion
    }
}