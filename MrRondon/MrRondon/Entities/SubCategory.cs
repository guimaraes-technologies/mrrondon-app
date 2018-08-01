using System.IO;
using Xamarin.Forms;

namespace MrRondon.Entities
{
    public class SubCategory
    {
        public int SubCategoryId { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }

        public ImageSource ImageSource { get { return Image == null ? ImageSource.FromFile("icon.png") : ImageSource.FromStream(() => new MemoryStream(Image)); } }

        public int? CategoryId { get; set; }
        public SubCategory Category { get; set; }
    }
}