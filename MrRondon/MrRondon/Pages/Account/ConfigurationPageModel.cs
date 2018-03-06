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
            ItemSelectedCommand = new Command<DistanceOptions>(ExecuteItemSelected);
        }

        private List<DistanceOptions> _items;
        public List<DistanceOptions> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public ICommand ItemSelectedCommand { get; set; }

        public void SetValue(double until)
        {
            var values = EnumExtensions.ConvertEnumToList<GetUntilOption>().ToList();
            Items = new List<DistanceOptions>();
            foreach (var item in values)
            {
                var distance = double.Parse(item.KeyValue);
                var isTheSame = distance.Equals(until);
                var distanceOption = new DistanceOptions(distance, item.Description, isTheSame);
                Items.Add(distanceOption);
            }
            ApplicationManager<object>.AddOrUpdate("PlaceUntil", until);
        }

        private void ExecuteItemSelected(DistanceOptions item)
        {
            SetValue(item.Distance);
        }
    }

    public class DistanceOptions
    {
        public DistanceOptions(double distance, string description, bool isChecked)
        {
            Distance = distance;
            Description = description;
            Icon = isChecked ? "check" : string.Empty;
        }

        public double Distance { get; private set; }
        public string Description { get; private set; }
        public string Icon { get; private set; }
    }

    public enum GetUntilOption
    {
        [EnumValueData(KeyValue = "100", Description = "100 metros")]
        Hundred,
        [EnumValueData(KeyValue = "300", Description = "300 metros")]
        ThreeHundred,
        [EnumValueData(KeyValue = "500", Description = "500 metros")]
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