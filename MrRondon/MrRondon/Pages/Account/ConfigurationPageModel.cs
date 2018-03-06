using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MrRondon.Auth;
using MrRondon.Extensions;
using MrRondon.Helpers;
using Xamarin.Forms;

namespace MrRondon.Pages.Account
{
    public class ConfigurationPageModel : BasePageModel
    {
        public ConfigurationPageModel()
        {
            Title = "Configurações";

            var until = ApplicationManager<object>.Find("PlaceUntil");

            var value = until == null ? AccountManager.DefaultSetting.PlaceUntil : double.Parse(until.ToString());
            SetValue(value);
            ItemSelectedCommand = new Command<EnumValueDataAttribute>((item) => ExecuteItemSelected(item));
        }

        private string _placeDescription;
        public string PlaceDescription
        {
            get => _placeDescription;
            set => SetProperty(ref _placeDescription, value);
        }

        public List<EnumValueDataAttribute> PlacesUntil { get; } = EnumExtensions.ConvertEnumToList<GetUntilOption>().ToList();

        public ICommand ItemSelectedCommand { get; set; }
        //public List<string> PlacesUntil { get; } = new List<string>(EnumExtensions.ConvertEnumToList<GetUntilOption>().ToList().Select(s => s.Description));

        //private int _placeUntilIndex;
        //public int PlaceUntilSelectedIndex
        //{
        //    get => _placeUntilIndex;
        //    set
        //    {
        //        if (_placeUntilIndex == value) return;

        //        _placeUntilIndex = value;
        //        Notify(nameof(PlaceUntilSelectedIndex));

        //        var selectedItem = EnumExtensions.GetEnumByType<GetUntilOption>(PlacesUntil[_placeUntilIndex]);
        //        PlaceDescription = selectedItem.Description;
        //        SetValue(double.Parse(selectedItem.KeyValue));
        //    }
        //}
        
        public void SetValue(double until)
        {
            ApplicationManager<object>.AddOrUpdate("PlaceUntil", until);
        }

        private void ExecuteItemSelected(EnumValueDataAttribute item)
        {
            PlaceDescription = item.Description;
            SetValue(double.Parse(item.KeyValue));
        }
    }

    public enum GetUntilOption
    {
        [EnumValueData(KeyValue = "100", Description = "100 metros")]
        Hundred,
        [EnumValueData(KeyValue = "100", Description = "300 metros")]
        ThreeHundred,
        [EnumValueData(KeyValue = "100", Description = "500 metros")]
        FiveHundred,
        [EnumValueData(KeyValue = "1000", Description = "1.000 metros")]
        Thousand,
        [EnumValueData(KeyValue = "3000", Description = "3.000 metros")]
        ThreeThousand,
        [EnumValueData(KeyValue = "5000", Description = "5.000 metros")]
        FiveThousand,
        [EnumValueData(KeyValue = "10000", Description = "10.000 metros")]
        TenThousand
    }
}