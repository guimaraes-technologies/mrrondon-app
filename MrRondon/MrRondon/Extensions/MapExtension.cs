using System.Collections.ObjectModel;
using Xamarin.Forms.Maps;

namespace MrRondon.Extensions
{
    public class MapExtension : Map
    {
        public ObservableCollection<PinExtension> Items { get; set; }
    }
}