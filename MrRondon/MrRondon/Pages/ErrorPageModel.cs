using System.IO;
using Xamarin.Forms;

namespace MrRondon.Pages
{
    public class ErrorPageModel : BasePageModel
    {
        public ErrorPageModel(string errorMessage, string title = "Erro", byte[] img = null)
        {
            Error = errorMessage;
            Title = title;
            SetImage(img);
        }

        private string _error;
        public string Error
        {
            get => _error;
            set => SetProperty(ref _error, value);
        }

        private string _imageString;
        public string ImageString
        {
            get => _imageString;
            set => SetProperty(ref _imageString, value);
        }

        private ImageSource _image;
        public ImageSource Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        private void SetImage(byte[] img)
        {
            Image = img == null || img.Length < 1 ? ImageSource.FromFile("sad_face") : ImageSource.FromStream(() => new MemoryStream(img));
        }
    }
}