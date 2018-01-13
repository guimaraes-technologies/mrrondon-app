using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MrRondon.Pages.Map
{
    public class MapPageModel : BasePageModel
    {
        public MapPageModel()
        {
            LoadPinsCommand = new Command(async () => await ExecuteLoadPins());
        }

        public ICommand LoadPinsCommand { get; set; }

        public List<Pin> Pins { get; set; } = new List<Pin>();

        private async Task ExecuteLoadPins()
        {
            var pins = new List<Pin>
            {
                new Pin
                {
                    Address = "Av. Farquar, 2866 - Panair, Porto Velho - RO, 76801-466",
                    Type = PinType.Place,
                    Label = "Centro Político e Administrativo de Rondônia - CPA"
                },
                new Pin
                {
                    Address = "Av. Pinheiro Machado, 696-724 - São Cristóvão, Porto Velho - RO, 76820-838",
                    Label = "Deep Club"
                },
                new Pin
                {
                    Address = "Av. Pinheiro Machado, 1356 - São Cristóvão, Porto Velho - RO, 76820-838",
                    Label = "Broadway Lounge nightclub",
                },
                new Pin
                {
                    Address = "R. Benjamin Constant, 1760 - São Cristóvão, Porto Velho - RO, 76804-072",
                    Label = "Public Haus",
                }
            };

            var geoCoder = new Geocoder();
            foreach (var pin in pins)
            {
                var approximateLocations = await geoCoder.GetPositionsForAddressAsync(pin.Address);
                if (approximateLocations == null) continue;
                var locations = approximateLocations as Position[] ?? approximateLocations.ToArray();
                if (locations.Length <= 0) continue;

                var latitude = locations.FirstOrDefault().Latitude;
                var longitude = locations.FirstOrDefault().Longitude;
                pin.Position = new Position(latitude, longitude);
                Pins.Add(pin);
            }

            await Task.Delay(1);
        }
    }
}