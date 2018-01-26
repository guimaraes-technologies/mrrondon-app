using System;
using System.IO;
using Xamarin.Forms;

namespace MrRondon.Entities
{
    public class HistoricalSight
    {
        public int HistoricalSightId { get; set; }
        public string Name { get; set; }
        public string SightHistory { get; set; }
        public byte[] Image { get; set; }

        public Guid AddressId { get; set; }
        public Address Address { get; set; }

        public ImageSource ImageSource { get { return Image == null ? ImageSource.FromFile("icon.png") : ImageSource.FromStream(() => new MemoryStream(Image)); } }
    }
}