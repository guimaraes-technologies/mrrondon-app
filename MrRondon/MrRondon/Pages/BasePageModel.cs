using MrRondon.Auth;
using MrRondon.Entities;
using MrRondon.Helpers;
using MrRondon.Services.Interfaces;
using MrRondon.ViewModels;
using Xamarin.Forms;

namespace MrRondon.Pages
{
    public class BasePageModel : ObservableObject
    {
        protected IMessageService MessageService;
        protected INavigationService NavigationService;

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

        private string _currentCity;
        public string CurrentCity
        {
            get => _currentCity;
            set => SetProperty(ref _currentCity, value);
        }

        protected BasePageModel()
        {
            IsPresented = false;
            IsLoading = false;
            Title = Constants.AppName;
            CurrentCity = ((City)Application.Current.Properties["city"])?.Name;
            MessageService = DependencyService.Get<IMessageService>();
            NavigationService = DependencyService.Get<INavigationService>();
        }

        public void SetCity(City city)
        {
            CurrentCity = city.Name;
        }
    }
}