using Xamarin.Forms;

namespace MrRondon.Pages.Event
{
    public partial class ListEventPage : ContentPage
    {
        public ListEventPage()
        {
            if (Device.RuntimePlatform.Equals(Device.iOS)) Icon = "ic_event";
            InitializeComponent();
        }
    }
}