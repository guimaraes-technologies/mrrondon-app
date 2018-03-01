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
        public byte[] Logo { get; set; }
        public byte[] Cover { get; set; }

        public Guid AddressId { get; set; }
        public Address Address { get; set; }

        public ImageSource ImageSourceLogo { get { return Logo == null ? ImageSource.FromFile("icon.png") : ImageSource.FromStream(() => new MemoryStream(Logo)); } }

        public ImageSource ImageSourceCover { get { return Cover == null ? ImageSource.FromFile("icon.png") : ImageSource.FromStream(() => new MemoryStream(Cover)); } }
    }
}