using MrRondon.Helpers;
using MrRondon.Pages;

namespace MrRondon.Entities
{
    public class City : ObservableObject
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