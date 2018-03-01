using System;
using System.IO;
using Xamarin.Forms;

namespace MrRondon.Entities
{
    public class Event
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public byte[] Logo { get; set; }
        public byte[] Cover { get; set; }

        public Guid AddressId { get; set; }
        public Address Address { get; set; }
        public bool IsFavorite { get; set; }

        public string GetValue => Value == 0 ? "Gratuito" : $"{Value:C}";
        public string RangeDateAndValue => $"Início: {StartDate.ToShortDateString()}\nTérminio: {EndDate.ToShortDateString()}\nValor: {GetValue}";
        public ImageSource ImageSourceLogo { get { return Logo == null ? ImageSource.FromFile("icon.png") : ImageSource.FromStream(() => new MemoryStream(Logo)); } }
        public ImageSource ImageSourceCover { get { return Cover == null ? ImageSource.FromFile("icon.png") : ImageSource.FromStream(() => new MemoryStream(Cover)); } }
    }
}