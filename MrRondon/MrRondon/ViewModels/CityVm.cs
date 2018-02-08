using MrRondon.Pages;

namespace MrRondon.ViewModels
{
    public class CityVm : BasePageModel
    {
        private int _cityId;
        public int CityId
        {
            get => _cityId;
            set => SetProperty(ref _cityId, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
    }
}