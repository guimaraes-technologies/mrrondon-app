using Xamarin.Forms;

namespace MrRondon.Pages
{
	public partial class MasterPage : MasterDetailPage
    {
		public MasterPage ()
		{
			InitializeComponent ();
            BindingContext = new MasterPageModel();
		}
	}
}