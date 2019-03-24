using Xamarin.Forms;

namespace MrRondon.Pages
{
    public partial class PermissionDeniedPage : ContentPage
    {
        public PermissionDeniedPage(string title, params string[] permissoes)
        {
            InitializeComponent();
            BindingContext = new PermissionDeniedPageModel(title, permissoes);
        }
    }
}