using System.IO;
using Xamarin.Forms;

namespace MrRondon.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public string ImageString { get; set; }

        public ImageSource GetImage { get { return string.IsNullOrWhiteSpace(ImageString) ? ImageSource.FromFile("icon.png") : ImageSource.FromStream(() => new MemoryStream(Image)); } }

        public int? SubCategoryId { get; set; }
        public Category SubCategory { get; set; }
    }
}