using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using MrRondon.Auth;
using MrRondon.Helpers;
using MrRondon.Pages.City;
using MrRondon.Services.Interfaces;
using Xamarin.Forms;

namespace MrRondon.Pages
{
    public class BasePageModel : ObservableObject
    {
        protected IMessageService MessageService;
        protected INavigationService NavigationService;

        public ICommand LoadCitiesCommand { get; set; }
        public ICommand ChangeActualCityCommand { get; set; }

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

        public ObservableRangeCollection<Entities.City> Cities { get; set; }
        public List<string> CityNames { get; set; }

        protected BasePageModel()
        {
            IsPresented = false;
            IsLoading = false;
            Title = Constants.AppName;
            CurrentCity = ApplicationManager<Entities.City>.Find("city") ?? AccountManager.DefaultSetting.City;
            Cities = new ObservableRangeCollection<Entities.City>();
            MessageService = DependencyService.Get<IMessageService>();
            NavigationService = DependencyService.Get<INavigationService>();
        }

        protected async Task ExecuteChangeActualCity(Page previousPage)
        {
            var pageModel = new ChangeCityPageModel(previousPage);
            await NavigationService.PushModalAsync(new ChangeCityPage(pageModel));
        }
    }
}