using System;
using System.IO;
using Xamarin.Forms;

namespace MrRondon.Entities
{
    public class Company
    {
        public Guid CompanyId { get; set; }

        public string Name { get; set; }

        public string FancyName { get; set; }

        public string Cnpj { get; set; }
        public byte[] Logo { get; set; }
        public byte[] Cover { get; set; }

        //Category or subcategory
        public int SegmentId { get; set; }
        public Category Segment { get; set; }

        public Guid AddressId { get; set; }
        public Address Address { get; set; }

        public string SegmentDescription => Segment == null ? "Não informado" : Segment.SubCategory == null ? Segment.Name : $"{Segment.Name} - {Segment.SubCategory.Name}";
        public string TelephoneAndEmail { get; set; }

        public ImageSource ImageSourceLogo { get { return Logo == null ? ImageSource.FromFile("icon.png") : ImageSource.FromStream(() => new MemoryStream(Logo)); } }
        public ImageSource ImageSourceCover { get { return Cover == null ? ImageSource.FromFile("icon.png") : ImageSource.FromStream(() => new MemoryStream(Cover)); } }
    }
}