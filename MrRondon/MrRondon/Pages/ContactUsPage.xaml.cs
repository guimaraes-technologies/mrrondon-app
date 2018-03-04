using Xamarin.Forms;

namespace MrRondon.Pages
{
	public partial class ContactUsPage : ContentPage
	{
		public ContactUsPage ()
		{
			InitializeComponent ();
            BindingContext = new ContactUsPageModel();
		}
	}
}