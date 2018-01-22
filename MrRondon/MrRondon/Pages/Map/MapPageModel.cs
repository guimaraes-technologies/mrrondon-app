using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MrRondon.Helpers;
using MrRondon.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MrRondon.Pages.Map
{
    public class MapPageModel : BasePageModel
    {
        public MapPageModel()
        {
            Title = Constants.AppName;
            LoadPinsCommand = new Command(async () => await ExecuteLoadPins());
        }

        public ICommand LoadPinsCommand { get; set; }

        public List<Pin> Pins { get; set; } = new List<Pin>();

        private async Task ExecuteLoadPins()
        {
            var service = new EventService();
            var events = await service.GetNearbyAsync();

            foreach (var item in events)
            {
                var pin = new Pin
                {
                    Id = item.AddressId,
                    Label = item.Name,
                    Address = item.Address.FullAddress,
                    Type = PinType.Place,
                    Position = new Position(item.Address.Latitude, item.Address.Longitude)
                };

                Pins.Add(pin);
            }
        }
    }
}